using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetFallingPlatforms : MonoBehaviour
{
    public GameObject[] platforms;
    private void OnTriggerEnter(Collider other) {
        for (int i=0;i<platforms.Length; i++)
            platforms[i].GetComponent<FallingPlatform>().resetPosition();
    }
}
