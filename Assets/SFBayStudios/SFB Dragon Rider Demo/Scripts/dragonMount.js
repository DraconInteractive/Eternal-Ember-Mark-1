#pragma strict

var humanModel			: GameObject;
var dragonRiderModel	: GameObject;
var demoCamera			: GameObject;
var demoCanvas			: GameObject;

var dragonAnimator		: Animator;

function OnTriggerEnter(other : Collider){
	if (other.tag == "Player")
	{
		humanModel.SetActive(false);
		dragonRiderModel.SetActive(true);
		demoCamera.SetActive(true);
		demoCanvas.SetActive(true);
		dragonAnimator.SetTrigger("mount");
	}
}