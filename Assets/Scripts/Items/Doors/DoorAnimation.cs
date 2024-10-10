using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    public AudioClip openDoorSound;
    public AudioClip closeDoorSound;
    int playNow = 0;
    AudioSource audioSource;
    Animator anim;
    void Start() {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); 
    }

    void Update() {
        if (playNow == 1) {
            audioSource.PlayOneShot(openDoorSound); 
            playNow = 0;
        }
        else if (playNow == 2) {
            audioSource.PlayOneShot(closeDoorSound); 
            playNow = 0;
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player")
            anim.Play("OpenDoor");
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player")
            anim.Play("CloseDoor");
    }

    public void playOpen() {
        playNow = 1;
    }
    public void playClose() {
        playNow = 2;
    }
}
