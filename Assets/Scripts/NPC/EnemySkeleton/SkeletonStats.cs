using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonStats : MonoBehaviour
{
    GameObject player;
    public float hitRange = 1f;
    public float hitDamage = 1f;
    public float attackSpeed = 2f;
    public bool isAttacking = false;
    public int HP;
    bool isAlive = true;
    Animator anim;

    void Start()
    {
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
        HP = UnityEngine.Random.Range(60, 100);
    }
    void Update()
    {
        
    }

    public void Hit(int hitDamage) {
        HP -= hitDamage;
        if (HP <= 0) 
            HP = 0;
        if (!isAlive)
            return;
        if (HP == 0) {
            isAlive = false;
            HP = 0;
            GetComponentInChildren<MeshCollider>().enabled = false;
            GetComponent<NavigatorAI>().enabled = false;

            if (GetComponent<NavMeshAgent>().enabled)
                GetComponent<NavMeshAgent>().isStopped = true;

            GetComponentInChildren<SkeletonAttack>().StopAllCoroutines();
            GetComponentInChildren<SkeletonAttack>().enabled = false;
            
            anim.Play("Die");
            player.GetComponent<PlayerStats>().addXP(7);
        }
    }

    public void onEndDeathAnimation() {
            GameObject.Destroy(gameObject);
            GetComponentInParent<Spawner>().EnemyDead();
    }

    public string GetText(float distance) {
        bool b = false;
        if (distance <= player.GetComponent<WeaponSystem>().equippedWeapon.range)
            b = true;
        return "HP: "+HP+"\nIn Range: "+b;
    }

    public void damagePlayer() {
        transform.gameObject.GetComponentInChildren<SkeletonAttack>().damagePlayer();
    }
}
