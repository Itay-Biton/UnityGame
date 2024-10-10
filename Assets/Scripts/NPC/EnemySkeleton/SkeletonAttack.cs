using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class SkeletonAttack : MonoBehaviour
{
    GameObject player;
    Animator anim;
    NavMeshAgent agent;
    NavigatorAI ai;
    public int attackDamage;
    public int attackRange = 1;

    void Start()
    {
        player = GameObject.Find("Player");
        anim = GetComponentInParent<Animator>();
        agent = GetComponentInParent<NavMeshAgent>();  
        ai = GetComponentInParent<NavigatorAI>();
        attackDamage = UnityEngine.Random.Range(10, 26);
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        GetComponent<MeshCollider>().enabled = false;
        agent.enabled = false;
        ai.enabled = false;
        anim.Play("Attack");
        StartCoroutine(AttackPlayer());
    }
    
    IEnumerator AttackPlayer()
    {
        yield return new WaitForSeconds(2f);
        GetComponent<MeshCollider>().enabled = true;
        agent.enabled = true;
        ai.enabled = true;
        anim.Play("Idle");
    }

    public void damagePlayer() {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < 3)
            player.GetComponent<PlayerStats>().TakeDamage(attackDamage);
    }
}

