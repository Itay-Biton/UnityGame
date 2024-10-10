using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSoundManager : MonoBehaviour
{
    public AudioClip fireSound;
    public AudioClip cantFireSound;
    public AudioClip equipSound;
    public AudioClip reloadSound;
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Load() {
        Start();
    }
    public void PlayFire() {
        audioSource.volume = 0.1f;
        audioSource.PlayOneShot(fireSound);
    }
    public void PlayCantFire() {
        audioSource.volume = 1f;
        audioSource.PlayOneShot(cantFireSound);
    }
    public void PlayReload() {
        audioSource.volume = 0.5f;
        audioSource.PlayOneShot(reloadSound);
    }
    public void PlayEquip() {
        audioSource.volume = 0.4f;
        audioSource.PlayOneShot(equipSound);
    }
}
