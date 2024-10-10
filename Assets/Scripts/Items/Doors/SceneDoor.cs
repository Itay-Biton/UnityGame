using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDoor : MonoBehaviour
{
    public Fade fadeScript;
    public Transform spawnPoint;
    public bool calledFade = false;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            calledFade = true;
            fadeScript.fadeIn();
        }
    }

    public void onFadeInComplite() {
        PersistentData.instance.Save();

        if (SceneManager.GetActiveScene().name ==  "Lobby") {
            SceneManager.LoadScene("Level");
        } else if (SceneManager.GetActiveScene().name ==  "Level") {
            SceneManager.LoadScene("Lobby");
        }
    }
    
}
