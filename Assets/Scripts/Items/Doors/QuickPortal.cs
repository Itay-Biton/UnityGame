using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickPortal : MonoBehaviour
{
    GameObject player;
    public Transform endPoint;
    void Start() {
        player = GameObject.Find("Player");
    }
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            player.transform.position = endPoint.position;
            player.transform.rotation = endPoint.rotation;
        }
    }
}
