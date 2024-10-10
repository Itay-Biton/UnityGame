using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalAnimation : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player")
            animator.SetTrigger("PortalTrigger");
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player")
            animator.SetTrigger("PortalTrigger");
    }
}
