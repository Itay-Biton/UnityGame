using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunLogic : MonoBehaviour
{
    public int ammo = 0;
    //public Text ammoText;
    public Transform source;
    public float maxRange;
    public int damage;
    public LayerMask layerOfEnemies;
    public LayerMask layerOfGuns;
    AudioPlayer audioPlayer;
    
    void Start()
    {
        //ammoText.text = "Ammo: " + ammo;
        audioPlayer = GetComponent<AudioPlayer>();
    }

    void Update()
    {
        // shoot logic
        if (Cursor.lockState == CursorLockMode.Locked && Input.GetKeyDown(KeyCode.Mouse0) && ammo > 0) {
            ammo--;
            //ammoText.text = "Ammo: " + ammo;
            audioPlayer.PlayGunShot();
            if (Physics.Raycast(source.position, source.forward, out RaycastHit hit1, maxRange, layerOfEnemies)) {
                if (hit1.collider.gameObject.CompareTag("Enemy")) {
                    hit1.collider.gameObject.GetComponent<SkeletonStats>().Hit(damage);
                }
            }
        }

        // equipe logic
        if (Physics.Raycast(source.position, source.forward, out RaycastHit hit2, maxRange, layerOfGuns)) {
            if (hit2.collider.gameObject.CompareTag("Gun")) {
                if (Input.GetKeyDown(KeyCode.E)) {
                    PickUp(hit2.collider.gameObject);
                }
            }
        }
    }
    public void addAmmo(int a) {
        ammo += a;
        //ammoText.text = "Ammo: " + ammo;
        audioPlayer.PlayEquipe();
    }
    public void PickUp(GameObject obj) {
        GameObject.Destroy(obj);
        addAmmo(4);
        audioPlayer.PlayEquipe();
    }
}
