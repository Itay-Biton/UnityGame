using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    Animator fadeAnimator;
    public SceneDoor sceneDoorScript;
    PlayerStats playerStatsScript;
    void Start() {
        fadeAnimator = GetComponent<Animator>();
        playerStatsScript = GameObject.Find("Player").GetComponent<PlayerStats>();
    }
    public void onFadeInComplite() {
        if (sceneDoorScript.calledFade) {
            sceneDoorScript.calledFade = false;
            sceneDoorScript.onFadeInComplite();
        }
        else if (playerStatsScript.calledFade) {
            playerStatsScript.calledFade = false;
            playerStatsScript.onFadeInComplite();
            fadeOut();
        }
    }
    public void fadeIn() {
        fadeAnimator.SetTrigger("FadeIn");
    }
    public void fadeOut() {
        fadeAnimator.SetTrigger("FadeOut");
    }
}
