using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SittingNPC : MonoBehaviour
{
    int rand = 0;
    Animator anim;
    string[] stateAnimator = new string[]{"Idle Sitting", "Laugh Sitting", "Talk Sitting",};
	enum MovementState {idle, laugh, talk};
	MovementState[] unstopableStates = new MovementState[]{MovementState.idle, MovementState.laugh, MovementState.talk};
	MovementState CurrentState = MovementState.idle;
	MovementState newState = MovementState.idle;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {   
        updateAnimation();
    }

    void updateAnimation() {
        if (AnimatorIsPlaying())
            return;
        else if (rand == 0 || rand == 2)
            newState = MovementState.idle;
        else if (rand == 1)
            newState = MovementState.laugh;
        else if (rand == 3)
            newState = MovementState.talk;
        
        rand++;
        if (rand > 4)
            rand = 0;
         
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
