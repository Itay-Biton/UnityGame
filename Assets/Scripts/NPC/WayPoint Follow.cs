using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class WayPointFollow : MonoBehaviour
{
    public Transform[] wayPoints;
    int targetIndex = 0;
    NavMeshAgent agent;
    float arrivalTime = 0;
    public float waitTime = 3f;
    public float detectionRange = 3f;

    bool isPlayerClose;
    GameObject player;
    
    Animator anim;
    string[] stateAnimator = new string[]{"Idle", "Walk", "Talk"};
	enum MovementState {idle, walk, talk};
	MovementState[] unstopableStates = new MovementState[]{};
	MovementState CurrentState = MovementState.idle;
	MovementState newState = MovementState.idle;

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        UpdateDestanetion();
    }

    void Update()
    {   
        if (Vector3.Distance(transform.position, wayPoints[targetIndex].position) < 1f) {
            IsPlayerClose();
            if (arrivalTime == 0) {
                arrivalTime = Time.time;
                agent.isStopped = true;
            }
            if (Time.time - arrivalTime > waitTime && !isPlayerClose) {
                NextWayPointIndex();
                UpdateDestanetion();
            }
            else if (isPlayerClose)
                LookAtPlayer();
        }

        updateAnimation();
    }

    void IsPlayerClose() {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < detectionRange) 
            isPlayerClose = true;
        else 
            isPlayerClose = false;
    }

    void LookAtPlayer() {
        Vector3 target_dir = (player.transform.position - transform.position);
        target_dir.y = 0;
        Vector3 new_dir = Vector3.RotateTowards(transform.forward, target_dir, 3*Time.deltaTime, 0);
        transform.rotation = Quaternion.LookRotation(new_dir);
    }

    void UpdateDestanetion() {
        agent.SetDestination(wayPoints[targetIndex].position); 
        agent.isStopped = false;
        arrivalTime = 0;
    }

    void NextWayPointIndex() {
        targetIndex++;
        if (targetIndex == wayPoints.Length)
            targetIndex = 0;
    }

    void updateAnimation() {
        if (AnimatorIsPlaying())
            return;
        else if (!agent.isStopped)
            newState = MovementState.walk;
        else if (isPlayerClose)
            newState = MovementState.talk;
        else
            newState = MovementState.idle;
        
		if (CurrentState == newState) 
			return;
		anim.Play(stateAnimator[(int)newState]);
		CurrentState = newState;
    }

    private bool AnimatorIsPlaying() { // unstopable animations (death, fall, jump...)
		if (unstopableStates.Contains(CurrentState) && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
    		return true;
		return false;
   	}
}
