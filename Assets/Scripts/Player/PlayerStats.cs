using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int XP = 0;
    public int health = 100;
    public int maxHealth = 100;
    public int potionCount = 0;
    Transform startSpawnPoint;
    Fade fadeScript;
    public bool calledFade = false;
    RectTransform healthBar;
    Text healthPoints;
    float healthBarWidth = 380f;
    float healthBarHight = 80f;
    public Rigidbody rb;
    Text xpText;
    Text potionText;
    AudioPlayer audioPlayer;
    Animator hitAlertAnimator;
    void Start() {
        hitAlertAnimator = GameObject.Find("HitAlert").GetComponent<Animator>();
        startSpawnPoint = GameObject.Find("SpawnPortal").transform;
        fadeScript = GameObject.Find("Fade").GetComponent<Fade>();
        healthBar = GameObject.Find("Health").GetComponent<RectTransform>();
        healthPoints = GameObject.Find("Health Points").GetComponent<Text>();
        xpText = GameObject.Find("XP Text").GetComponent<Text>();
        potionText = GameObject.Find("Potions Text").GetComponent<Text>();
        audioPlayer = GetComponent<AudioPlayer>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        setUI();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.F) && potionCount>0 && health<maxHealth)
            TakePotion();
    }
    public void addXP(int amount){
        XP += amount;
        setUI();
    }
    public void AddPotion() {
        potionCount++;
        setUI();
    }
    void TakePotion() {
        potionCount--;
        //play potion sound
        heal(10);
        setUI();
    }
    public void heal(int h) {
        health += h;
        if (health > maxHealth)
            health = maxHealth;
        setUI();
    }
    public void TakeDamage(int damege) {
        health -= damege;
        hitAlertAnimator.SetTrigger("Alert");
        audioPlayer.PlayHit();
        if (health <= 0 && !calledFade)
            Kill();
        setUI();
    }
    public void Kill() {
        health = 0;
        setUI();
        Respawn();
    }
    void Respawn() {
        calledFade = true;
        fadeScript.fadeIn();
        rb.isKinematic = true;
    }
    public void onFadeInComplite() {
        transform.position = startSpawnPoint.position;
        health = maxHealth;
        rb.isKinematic = false;
        setUI();
    }

    void setUI() {
        healthBar.sizeDelta = new Vector2(((float)health/maxHealth) * healthBarWidth, healthBarHight);
        healthPoints.text = health.ToString();

        xpText.text = "XP: "+XP;
        potionText.text = "Potions: "+potionCount;
    }
}
