using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Text outputText;
    GameObject player;
    PlayerStats playerStatsScript;
    WeaponSystem ws;
    GameObject shop;
    void Start()
    {
        player = GameObject.Find("Player");
        playerStatsScript = player.GetComponent<PlayerStats>();
        ws = player.GetComponent<WeaponSystem>();

        shop = GameObject.Find("Shop");
        shop.SetActive(false);
    }
    void Update() {
        if (shop.activeSelf) {
            if (Input.GetKeyDown(KeyCode.Q))
                CloseShop();
        }
    }
    public void OpenShop() {
        shop.SetActive(true);
        playerStatsScript.rb.isKinematic = true;
        player.GetComponent<FirstPersonController>().lockCamera = true;
        Cursor.lockState = CursorLockMode.None;
        outputText.text = "";
    }
    public void Buy(int optNum) {
        switch (optNum) {
            case 0: // 3 ammo for 3xp
                if (playerStatsScript.XP >= 3) {
                    playerStatsScript.addXP(-3);
                    Weapon.AddAmmo(3);
                    outputText.text = "Success!";
                }
                else 
                    outputText.text = "Not enough XP";
                break;
            case 1: // 6 ammo for 5xp
                if (playerStatsScript.XP >= 5) {
                    playerStatsScript.addXP(-5);
                    Weapon.AddAmmo(6);
                    outputText.text = "Success!";
                }
                else 
                    outputText.text = "Not enough XP";
                break;
            case 2: // 9 ammo for 7xp
                if (playerStatsScript.XP >= 7) {
                    playerStatsScript.addXP(-7);
                    Weapon.AddAmmo(9);
                    outputText.text = "Success!";
                }
                else 
                    outputText.text = "Not enough XP";
                break;
            case 3: //  Health Potion 10HP for 10xp
                if (playerStatsScript.XP >= 10) {
                    playerStatsScript.addXP(-10);
                    playerStatsScript.AddPotion();
                    outputText.text = "Success!";
                }
                else 
                    outputText.text = "Not enough XP";
                break;
            default:
                break;
        }

    }

    public void CloseShop() {
        shop.SetActive(false);
        playerStatsScript.rb.isKinematic = false;
        player.GetComponent<FirstPersonController>().lockCamera = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public bool isShopActive() {
        return shop.activeSelf;
    }
}
