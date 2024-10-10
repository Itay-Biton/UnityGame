using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Interactee : MonoBehaviour
{
    public void interact() {
        if (Input.GetKeyDown(KeyCode.E))
            Destroy(gameObject);
    }
}
