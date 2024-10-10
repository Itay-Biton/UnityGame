using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSystem : MonoBehaviour
{
    static int ID= 0;
    int id = ID++;
    public Transform gunHolder; 
    public int currentWeaponIndex; 

    public List<int> indexes = new List<int>();
    public Weapon equippedWeapon; 
    public Transform source;
    public LayerMask layerOfGuns;
    public int pickUpRange = 5;

    void Start()
    {
        for (int j=0;j<gunHolder.childCount;j++) {
            gunHolder.GetChild(j).gameObject.GetComponent<Weapon>().Load();
            gunHolder.GetChild(j).gameObject.SetActive(false);
        }

        if (indexes.Count == 0) {
            indexes.Add(0);
            EquipWeapon(0);
        }
        else
            EquipWeapon(currentWeaponIndex);
        
    }

    void Update()
    {
        if (Cursor.lockState == CursorLockMode.Locked && !equippedWeapon.Busy()) {
            if (!equippedWeapon.isAuto && Input.GetKeyDown(KeyCode.Mouse0)) 
                StartCoroutine(equippedWeapon.Fire(source));
            else if (equippedWeapon.isAuto && Input.GetKey(KeyCode.Mouse0))
                StartCoroutine(equippedWeapon.Fire(source));
        }

        if (Input.GetKeyDown(KeyCode.R) && equippedWeapon.CanReload())
            StartCoroutine(equippedWeapon.Reload());
        

        if (Input.GetKeyDown(KeyCode.Alpha1)) // hands
            EquipWeapon(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2)) // slot 1
            EquipWeapon(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            EquipWeapon(2);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            EquipWeapon(3);
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            EquipWeapon(4);
        else if (Input.GetKeyDown(KeyCode.Alpha6))
            EquipWeapon(5);

        if (Physics.Raycast(source.position, source.forward, out RaycastHit hit, pickUpRange, layerOfGuns)) {
            if (hit.collider.gameObject.CompareTag("Gun")) {
                if (Input.GetKeyDown(KeyCode.E)) {
                    PickUp(hit.collider.gameObject);
                }
            }
        }
    }
    public void Load() {
        Start();
    }
    void PickUp(GameObject gunObject) {
        Weapon.AddAmmo(gunObject.GetComponent<Weapon>().clipSize);
        bool isNew = true;
        int i = -1;

        foreach (int index in indexes) {
            if (gunObject.GetComponent<Weapon>().weaponID == gunHolder.GetChild(index).GetComponent<Weapon>().weaponID) {
                isNew = false;
                i = index;
                break;
            }
        }
        if (isNew) {
            for (int j=0;j<gunHolder.childCount;j++) {
                if (gunObject.GetComponent<Weapon>().weaponID == gunHolder.GetChild(j).GetComponent<Weapon>().weaponID) {
                    i = j;
                    break;
                }
            }
            indexes.Add(i);
        }
        GameObject.Destroy(gunObject);
        EquipWeapon(indexes.IndexOf(i));
    }

    void EquipWeapon(int weaponIndex) {
        if (weaponIndex >= indexes.Count)
            return;
        gunHolder.GetChild(currentWeaponIndex).gameObject.SetActive(false);

        currentWeaponIndex = indexes[weaponIndex];
        gunHolder.GetChild(currentWeaponIndex).gameObject.SetActive(true);
        equippedWeapon = gunHolder.GetChild(currentWeaponIndex).GetComponent<Weapon>();
        equippedWeapon.UpdateGUI();
        equippedWeapon.weaponSoundManager.PlayEquip();
    }

}
