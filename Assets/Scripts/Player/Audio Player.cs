using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    // Public variables
    public AudioClip[] grassFootsteps;
    public AudioClip[] woodFootsteps;
    public AudioClip[] roadFootsteps;
    public AudioClip gunShotSound;
    public AudioClip equipeSound;
    public AudioClip hitSound;
    public float stepInterval = 0.5f;
    public LayerMask groundLayer;
    FirstPersonController firstPersonControllerScript;
    AudioSource audioSource;

    float lastStepTime = 0f;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
        firstPersonControllerScript = GetComponent<FirstPersonController>();
    }
    void Update()
    {
        if (firstPersonControllerScript.isWalking)
            stepInterval = 0.5f;
        else if (firstPersonControllerScript.isSprinting)
            stepInterval = 0.15f;
        if (Time.time - lastStepTime >= stepInterval)
        {
            if (firstPersonControllerScript.isGrounded)
            {
                if (firstPersonControllerScript.isWalking || firstPersonControllerScript.isSprinting) {
                    PlayFootstepSound();
                    lastStepTime = Time.time;
                }
            }
        }
    }

    void PlayFootstepSound() {
        string surfaceTag = "";
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f, groundLayer))
            surfaceTag = hit.collider.gameObject.tag;

        AudioClip[] chosenSounds;
        switch (surfaceTag) {
            case "Grass":
                chosenSounds = grassFootsteps;
                break;
            case "Wood":
                chosenSounds = woodFootsteps;
                break;
            case "Road":
                chosenSounds = roadFootsteps;
                break;
            default:
                // Use a default sound or no sound
                chosenSounds = new AudioClip[0];
                break;
        }

        if (chosenSounds.Length > 0) {
            int randomIndex = Random.Range(0, chosenSounds.Length);
            audioSource.volume = 0.1f;
            audioSource.PlayOneShot(chosenSounds[randomIndex]);
        }
    }

    public void PlayGunShot() {
        audioSource.volume = 0.1f;
        audioSource.PlayOneShot(gunShotSound);
    }
    public void PlayHit() {
        audioSource.volume = 0.5f;
        audioSource.PlayOneShot(hitSound);
    }
    public void PlayEquipe() {
        audioSource.volume = 0.4f;
        audioSource.PlayOneShot(equipeSound);
    }
}
