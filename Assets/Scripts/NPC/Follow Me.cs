using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.VisualScripting;
using System;

public class FollowMe : MonoBehaviour
{
    GameObject player;
    public float detectionRange = 3f;
    public int hitRange = 1;
    public int hitDamage = 1;
    public float attackSpeed = 2f;
    public bool isAttacking = false;
    int HP = 10;
    Animator anim;
    AudioSource audioSource;
    NavMeshAgent agent;

    void Start()
    {
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();  
        audioSource = GetComponent<AudioSource>();
        HP = UnityEngine.Random.Range(5, 16);
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        bool isChasing = distance < detectionRange && distance > hitRange;
        bool inAttackRange = distance < hitRange;

        if (isChasing) {
            agent.SetDestination(player.transform.position);
            LookAtPlayer();
            agent.isStopped = false;
            anim.SetBool("Walking",true);
        }
        else {
            agent.isStopped = true;
            anim.SetBool("Walking",false);
        }

        if (inAttackRange && !isAttacking)
            StartCoroutine(AttackPlayer());
    }

    void LookAtPlayer() {
        Vector3 target_dir = (player.transform.position - transform.position);
        target_dir.y = 0;
        Vector3 new_dir = Vector3.RotateTowards(transform.forward, target_dir, 3*Time.deltaTime, 0);
        transform.rotation = Quaternion.LookRotation(new_dir);
    }   
    IEnumerator AttackPlayer()
    {
        isAttacking = true;
        anim.SetTrigger("Attacking");
        audioSource.Play();
        player.GetComponent<PlayerStats>().TakeDamage(hitDamage);
        yield return new WaitForSeconds(attackSpeed);
        isAttacking = false;
    }

    public void Hit(int hitDamage) {
        HP -= hitDamage;
        if (HP <= 0) {
            GameObject.Destroy(gameObject);
            player.GetComponent<PlayerStats>().addXP(7);
            GetComponentInParent<Spawner>().EnemyDead();
        }
    }

    public string GetText(float distance) {
        bool b = false;
        if (distance <= player.GetComponent<GunLogic>().maxRange)
            b = true;
        return "HP: "+HP+"\nIn Range: "+b;
    }
}
