using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestionBoard : MonoBehaviour
{
    public GameObject wall;
    PlayerStats playerStatsScript;
    public TextMeshPro q;
    public TextMeshPro a;

    void Start() {
        playerStatsScript = GameObject.Find("Player").GetComponent<PlayerStats>();
    }
    public void AnswerPressed(TextMeshPro ans) {
        if (ans == a) {
            GameObject.Destroy(wall);
            playerStatsScript.addXP(5);
        }
        else
            playerStatsScript.TakeDamage(20);
    }
}
