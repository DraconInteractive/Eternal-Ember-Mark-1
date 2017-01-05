#pragma strict

var navMeshAgent		: UnityEngine.AI.NavMeshAgent;						// Agent for this character
var charAnimator		: Animator;							// Animator for this character
var moveOffset			: float				= 1.0;			// Offset for movement value

function Update () {
	charAnimator.SetFloat("locomotion", (Vector3.Distance(navMeshAgent.velocity, Vector3(0,0,0))) + moveOffset);		// Pass the value to the animator
}

function SetNavMeshDestination(newTransform : Transform){
	navMeshAgent.destination	= newTransform.position;
}