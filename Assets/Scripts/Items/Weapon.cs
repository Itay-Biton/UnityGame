using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public static int totalAmmo = 0;
    static Text ammoText;

    public int weaponID;
    public int ammoClip = 0;
    public int clipSize;
    public float fireRate; 
    public float reloadTime;
    public int damage;
    public float range;
    public LayerMask layerMask;
    public bool isAuto;

    AudioSource audioSource;
    Animator anim;
    public Text ammoClipText;
    public bool reloading = false;
    public bool fireing = false;
    public WeaponSoundManager weaponSoundManager;

    void Start()
    {
        ammoText = GameObject.Find("Ammo Text").GetComponent<Text>();
        ammoClipText = GameObject.Find("AmmoClip Text").GetComponent<Text>();
        try {
            weaponSoundManager = GetComponent<WeaponSoundManager>();
            weaponSoundManager.Load();
            anim = GetComponent<Animator>();
            UpdateGUI();
        } catch {}
    }
    public void Load() {
        Start();
    }
    public bool CanFire()
    {
        return ammoClip > 0 || clipSize == 0;
    }
    public bool Busy() {
        return fireing || reloading;
    }

    public IEnumerator Fire(Transform source)
    {
        if (!CanFire()) {
            weaponSoundManager.PlayCantFire();
            yield break;
        }
        fireing = true;
        anim.SetTrigger("Fire");
        weaponSoundManager.PlayFire();
        ammoClip--;
        UpdateGUI();
        if (Physics.Raycast(source.position, source.forward, out RaycastHit hit, range))
            if (hit.collider.gameObject.CompareTag("Enemy"))
                hit.collider.gameObject.GetComponent<SkeletonStats>().Hit(damage);
            else {
                Instantiate(PersistentData.instance.decal, hit.point + (hit.normal * 0.01f), Quaternion.LookRotation(hit.normal));
            }
        yield return new WaitForSeconds(fireRate);
        fireing = false;
    }

    public bool CanReload()
    {
        return clipSize > 0 && ammoClip < clipSize && totalAmmo > 0;
    }
    public IEnumerator Reload() {
        if (!CanReload()) {
            // play cant reload sound
            yield return new WaitForSeconds(0.1f);
        }
        reloading = true;
        weaponSoundManager.PlayReload();
        yield return new WaitForSeconds(reloadTime);
        int bulletsToReload = clipSize - ammoClip;
        if (totalAmmo >= bulletsToReload) {
            totalAmmo -= bulletsToReload;
            ammoClip += bulletsToReload;
        }
        else {
            ammoClip += totalAmmo;
            totalAmmo = 0;
        }
        UpdateGUI();
        reloading = false;
    }

    public static void AddAmmo(int a) {
        totalAmmo += a;
        ammoText.text = "Total Ammo: " + totalAmmo; 
    }

    public void UpdateGUI() {
        ammoText.text = "Total Ammo: " + totalAmmo; 
        if (clipSize > 0)
            ammoClipText.text = ammoClip +"/"+clipSize;
        else
            ammoClipText.text = "X";
    }
}
