#pragma strict

var animator			: Animator;

function Start () {
	animator	= GetComponent.<Animator>();
}

function SetLocomotion(newValue : float){
	animator.SetFloat("locomotion", newValue);
}

function SetTurning(newValue : float){
	animator.SetFloat("turning", newValue);
}