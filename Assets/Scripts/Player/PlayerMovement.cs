using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {
	
	public CharacterController controller;
    public float speed = 10f;
    public float gravity = -70f;
    public float jumpHight = 3f;
    public Transform groundChecker;
    public float groundDistance = 0.02f;
    public LayerMask groundMask;

    public bool isGrounded;
    float yVelocity = -5f;
	public Vector3 move;

    Animator anim;
    string[] stateAnimator = new string[]{"Idle", "Walk", "Run", "Jump", "Die"};
	enum MovementState {idle, walk, run, jump, die};
	//MovementState[] unstopableStates = new MovementState[]{MovementState.dash, MovementState.attack, MovementState.jumpAttack, MovementState.die, MovementState.hurt};
	MovementState CurrentState = MovementState.idle;
	MovementState newState = MovementState.idle;

    void Start() {
        anim = transform.Find("Skeleton Red eye").GetComponent<Animator>();
    }

	void Update () {
        float dx = Input.GetAxis("Horizontal");
        float dz = Input.GetAxis("Vertical");
        move = (transform.right*dx + transform.forward*dz) * speed; // movement in 2D

        isGrounded = IsGrounded();
        if (isGrounded && yVelocity < 0) {
            yVelocity = -0.1f; // keep it on the ground
        }
        if (isGrounded && Input.GetButtonDown("Jump")) {
            yVelocity =  Mathf.Sqrt(jumpHight * gravity * -2f); // give upwards velocity to jump
        }
        yVelocity += gravity * Time.deltaTime; // slow down jump because gravity
        move.y = yVelocity;

        controller.Move(move * Time.deltaTime);
        updateAnimation();
	}

    bool IsGrounded() {
        return Physics.CheckSphere(groundChecker.position, groundDistance, groundMask);
    }

    void updateAnimation() {
        if (!isGrounded)
			newState = MovementState.jump;
		else if (move.z != 0)
			newState = MovementState.walk;
		else 
			newState = MovementState.idle;
		ChangeAnimationState(newState);
    }

    void ChangeAnimationState(MovementState state) {
        if (CurrentState == state) 
			return;
		anim.Play(stateAnimator[(int)state]);
		CurrentState = state;
    }
}

