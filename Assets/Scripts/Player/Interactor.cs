using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Interactor : MonoBehaviour
{
    Transform source;
    public float maxRange;
    Text infoText;
    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
        source = GameObject.Find("PlayerCamera").transform;
        infoText = GameObject.Find("InfoText").GetComponent<Text>();
    }

    void Update()
    {
        String text = null;
        if (Physics.Raycast(source.position, source.forward, out RaycastHit hit, maxRange)) {
            if (hit.collider.gameObject.CompareTag("Enemy")) {
                text = hit.collider.gameObject.GetComponent<SkeletonStats>().GetText(hit.distance);
                if (player.GetComponent<WeaponSystem>().equippedWeapon.ammoClip == 0)
                    text += "\nNo Ammo";
            } 
            else if (hit.collider.gameObject.CompareTag("Gun")) {
                text = "Press E to pick up";
            }
            else if (hit.collider.gameObject.CompareTag("QuestionButton")) {
                text = "Press E to push";
                if (Input.GetKeyDown(KeyCode.E))
                    hit.collider.gameObject.GetComponentInParent<QuestionBoard>().AnswerPressed(hit.collider.gameObject.GetComponent<TextMeshPro>());
            }
            else if (hit.collider.gameObject.CompareTag("Lava")) {
                text = "LAVA - Instat death";
            }
            else if (hit.collider.gameObject.CompareTag("Falling Platform")) {
                text = "Falling Platform\n Fall Delay: "+hit.collider.gameObject.GetComponent<FallingPlatform>().fallDelay;
            }
            else if (hit.collider.gameObject.CompareTag("Shop")) {
                text = "Press E to open the shop";
                ShopManager s = hit.collider.gameObject.GetComponentInParent<ShopManager>();
                if (!s.isShopActive() && Input.GetKeyDown(KeyCode.E))
                    s.OpenShop();
            }
        }
        if (text != null) {
            infoText.enabled = true;
            infoText.text = text;
        }
        else 
            infoText.enabled = false;
    }
}
