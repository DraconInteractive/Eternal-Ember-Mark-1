using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackBehaviour : StateMachineBehaviour {
	public int attackNum;
	Player player;

	public bool isSpell;
	public int spellNum;
	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (isSpell) {
			animator.SetBool ("sOneCA", false);
		} else {
			animator.SetBool ("ContinueAttack", false);
			player = Player.player;
			player.currentAttack = attackNum;
		}


	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		if (isSpell) {
			if (spellNum == 1) {
				if (Input.GetKeyDown(KeyCode.Alpha1)) {
					animator.SetBool ("sOneCA", true);
					animator.ResetTrigger ("CastSpell");
				}
			}
		} else {
			if (Input.GetMouseButtonDown(0)) {
				animator.SetBool ("ContinueAttack", true);
				animator.ResetTrigger ("Attack");
			}
		}

	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (isSpell) {
			
		} else {
			player.currentAttack = 0;
		}

	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
