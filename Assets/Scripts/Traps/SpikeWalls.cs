using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeWalls : MonoBehaviour
{
    PlayerStats playerStatsScript;
    public GameObject wall1;
    public GameObject wall2;
    public float speed = 1f;
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = 1/speed;
        playerStatsScript = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            anim.Play("CloseWalls");
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            Reset();
        }
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Player")) {
            playerStatsScript.Kill();
        }
    }

    void Reset() {
        anim.Play("CloseWalls", -1, 1f);
        anim.Play("OpenedWalls");
    }
}
