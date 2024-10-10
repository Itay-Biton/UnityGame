using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;


//using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PersistentData : MonoBehaviour
{
    public static PersistentData instance = null;
    public GameObject decal;
    GameObject menu;
    GameObject player;

    // Weapon system
    static List<int> ammoClips = new List<int>();
    static List<int> indexes = new List<int>();
    static int currentWeaponIndex;

    // Player Stats
    static int XP;
    static int potionCount;
    static int health;
    static int maxHealth;
    void Start()
    {   
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
            setObjects();
        }
        else {
            Destroy(gameObject);
            setObjects();
            player.GetComponent<WeaponSystem>().indexes = indexes;
            player.GetComponent<WeaponSystem>().currentWeaponIndex = currentWeaponIndex;
            for (int i=0; i<indexes.Count; i++)
                player.GetComponent<WeaponSystem>().gunHolder.GetChild(indexes[i]).GetComponent<Weapon>().ammoClip = ammoClips[i];
            player.GetComponent<PlayerStats>().maxHealth = maxHealth;
            player.GetComponent<PlayerStats>().health = health;
            player.GetComponent<PlayerStats>().XP = XP;
            player.GetComponent<PlayerStats>().potionCount = potionCount;
        }
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.M)) {
            if (menu.activeSelf) {
                menu.SetActive(false);
                player.GetComponent<PlayerStats>().rb.isKinematic = false;
                player.GetComponent<FirstPersonController>().lockCamera = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else {
                menu.SetActive(true);
                player.GetComponent<PlayerStats>().rb.isKinematic = true;
                player.GetComponent<FirstPersonController>().lockCamera = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    void setObjects() {
        instance.player = GameObject.Find("Player");
        instance.menu = GameObject.Find("Menu");
        player = instance.player;
        menu = instance.menu;
        menu.SetActive(false);
    }

    public void Save() {
        indexes = player.GetComponent<WeaponSystem>().indexes;
        currentWeaponIndex = player.GetComponent<WeaponSystem>().currentWeaponIndex;
        ammoClips.Clear();
        for (int i=0; i<indexes.Count; i++) 
            ammoClips.Add(player.GetComponent<WeaponSystem>().gunHolder.GetChild(indexes[i]).GetComponent<Weapon>().ammoClip);
        
        XP = player.GetComponent<PlayerStats>().XP;
        potionCount = player.GetComponent<PlayerStats>().potionCount;
        health = player.GetComponent<PlayerStats>().health;
        maxHealth = player.GetComponent<PlayerStats>().maxHealth;
    }

    public bool isMenuActive() {
        return menu.activeSelf;
    }
    public GameObject GetDecalObj() {
        return decal;
    }

    static public void QuitGame() {
        #if (UNITY_EDITOR)
            UnityEditor.EditorApplication.isPlaying = false;
        #else
           Application.Quit();
        #endif
    }
}
