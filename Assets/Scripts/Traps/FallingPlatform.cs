using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FallingPlatform : MonoBehaviour
{
    public float fallDelay = 0.5f;
    Rigidbody rb;
    Vector3 initialPosition;
    Quaternion initialRotation;
    void Start() {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; 
    }
    void Update() {
        if (!rb.isKinematic)
            rb.AddForce(Vector3.up * 1.15f, ForceMode.Acceleration);
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            StartCoroutine(FallAfterDelay());
        }
    }

    public void resetPosition() {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        rb.isKinematic = true;
    }
    IEnumerator FallAfterDelay() {
        yield return new WaitForSeconds(fallDelay); 
        rb.isKinematic = false; 
    }
}

