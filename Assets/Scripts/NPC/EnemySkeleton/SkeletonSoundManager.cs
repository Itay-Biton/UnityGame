using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSoundManager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip swing;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void AttackSound() {
        audioSource.PlayOneShot(swing);
    }
}
