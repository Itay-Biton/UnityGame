using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NavigatorAI : MonoBehaviour
{
    GameObject player;
    Animator anim;
    NavMeshAgent agent;
    public int detectionRange = 8;
    void Start()
    {
        player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        agent.SetDestination(player.transform.position);
        agent.isStopped = false;
        anim.SetBool("Walking", true);

        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance > detectionRange) {
            agent.isStopped = true;
            anim.SetBool("Walking", false);
        }
        else
            LookAtPlayer();
    }

    void LookAtPlayer() {
        Vector3 target_dir = (player.transform.position - transform.position);
        target_dir.y = 0;
        Vector3 new_dir = Vector3.RotateTowards(transform.forward, target_dir, 3*Time.deltaTime, 0);
        transform.rotation = Quaternion.LookRotation(new_dir);
    }
}
