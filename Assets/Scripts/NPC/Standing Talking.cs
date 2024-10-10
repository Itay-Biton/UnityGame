using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingTalking : MonoBehaviour
{
    GameObject player;
    public float detectionRange = 3f;

    bool isPlayerClose;
    Vector3 initPos;
    Quaternion initRot;
    
    Animator anim;
    string[] stateAnimator = new string[]{"Idle", "Talk"};
	enum MovementState {idle, talk};
	MovementState CurrentState = MovementState.idle;
	MovementState newState = MovementState.idle;

    void Start()
    {
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
        initPos = transform.position;
        initRot = transform.rotation;
    }

    void Update()
    {
        IsPlayerClose();
        if (isPlayerClose) 
            LookAtPlayer();
        else
            Home();
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

    void Home() {
        transform.position = initPos;
        transform.rotation = initRot;
    }

    void updateAnimation() {
        if (isPlayerClose)
			newState = MovementState.talk;
		else 
			newState = MovementState.idle;

		if (CurrentState == newState) 
			return;
		anim.Play(stateAnimator[(int)newState]);
		CurrentState = newState;
    }
}
