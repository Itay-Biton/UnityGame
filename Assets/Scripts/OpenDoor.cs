using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject doorPivot;
    public bool isManual = true;
    public float openDegree = 150f;
    public bool flipped = false;

    bool isOpen = false;
    bool toggled = false; // keep track on player open/close request
    bool youAreClose = false; // if player is in trigger radius

    void Awake() {
        //transform.localScale = new Vector3(2,2,2); // door size problem
        if (flipped) // door open inside or outside
            openDegree = -openDegree;
    }

    void Update() {
        if (youAreClose && Input.GetKeyDown(KeyCode.E))
            toggled = true;
    }

    void OnTriggerStay(Collider other) {
        if (isManual && other.gameObject.tag == "Player" && toggled) {
            if (isOpen)
                Close();
            else
                Open();
            toggled = false;
        }
    }

    void OnTriggerEnter(Collider other) {
        youAreClose = true;
        if (!isManual && other.gameObject.tag == "Player")
            Open();
    }

    void OnTriggerExit(Collider other) {
        youAreClose = false;
        if (!isManual && other.gameObject.tag == "Player")
            Close();
    }

    void Close() { // rotate -openDegree
        doorPivot.transform.eulerAngles = new Vector3(doorPivot.transform.eulerAngles.x, doorPivot.transform.eulerAngles.y - openDegree, doorPivot.transform.eulerAngles.z);
        isOpen = false;
    }

    void Open() { // rotate +openDegree
        doorPivot.transform.eulerAngles = new Vector3(doorPivot.transform.eulerAngles.x, doorPivot.transform.eulerAngles.y + openDegree, doorPivot.transform.eulerAngles.z);
        isOpen = true;
    }
}
