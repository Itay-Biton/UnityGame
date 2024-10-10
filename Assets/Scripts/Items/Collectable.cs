using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Collectable : MonoBehaviour
{
    GameObject player;
    public float respawnTime = 20f;
    float collectedTime;
    // items
    const string ammoTag = "Ammo";
    const string healthPotionTag = "Health Potion";
    SphereCollider sphereCollider;
    MeshCollider meshCollider;
    MeshRenderer meshRenderer;
    //public Text itemTextCount;
    public AudioSource collectionAudioSource;
    void Start() {
        player = GameObject.Find("Player");
        sphereCollider = GetComponent<SphereCollider>();
        meshCollider = GetComponent<MeshCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
    }
    void Update() {
        if (Time.time - collectedTime >= respawnTime){
            sphereCollider.enabled = true;
            meshCollider.enabled = true;
            meshRenderer.enabled = true;
        }
    }
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            collectionAudioSource.Play();
            sphereCollider.enabled = false;
            meshCollider.enabled = false;
            meshRenderer.enabled = false;
            collectedTime = Time.time;
            switch (gameObject.tag) {
                case ammoTag:
                    Weapon.AddAmmo(3);
                    break;
                case healthPotionTag:
                    player.GetComponent<PlayerStats>().AddPotion();
                    break;
                default:
                    break;
            }
        }
    }
}