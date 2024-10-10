using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class SitHere : MonoBehaviour
{
    public Transform[] wayPoints;
    int targetIndex = 0;
    NavMeshAgent agent;
    float arrivalTime = 0;
    public float waitTime = 3f;

    int sitState; // 1-sit 2-sit_idle 3-standup
    Animator anim;
    string[] stateAnimator = new string[]{"Idle", "Walk", "Sit", "Standup", "Idle Sitting"};
	enum MovementState {idle, walk, sit, standup, idleSitting};
	MovementState[] unstopableStates = new MovementState[]{MovementState.sit, MovementState.standup};
	MovementState CurrentState = MovementState.idle;
	MovementState newState = MovementState.idle;

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        UpdateDestanetion();
    }

    void Update()
    {   
        if (Vector3.Distance(transform.position, wayPoints[targetIndex].position) < 0.5f) {
            if (arrivalTime == 0) {
                LookAt(wayPoints[targetIndex], false);
                agent.isStopped = true;
                agent.velocity = Vector3.zero;
                if (sitState == 1 && !AnimatorIsPlaying())
                    arrivalTime = Time.time;
                sitState = 1;
            }
            else {
                if (Time.time - arrivalTime > waitTime) {
                    if (sitState == 3 && !AnimatorIsPlaying()) {
                        NextWayPointIndex();
                        UpdateDestanetion();
                    }
                    else
                        sitState = 3;
                }
                else {
                    LookAt(wayPoints[targetIndex], false);
                    sitState = 2;
                }
            }
        }

        updateAnimation();
    }

    void LookAt(Transform obj, bool forward) {
        Vector3 target_dir = (obj.position - transform.position);
        target_dir.y = 0;
        if (!forward)
            target_dir = -target_dir;
        Vector3 new_dir = Vector3.RotateTowards(transform.forward, target_dir, 6*Time.deltaTime, 0);
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
        else if (sitState == 1) 
            newState = MovementState.sit;
        else if (sitState == 2) 
            newState = MovementState.idleSitting;
        else if (sitState == 3) 
            newState = MovementState.standup;
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
