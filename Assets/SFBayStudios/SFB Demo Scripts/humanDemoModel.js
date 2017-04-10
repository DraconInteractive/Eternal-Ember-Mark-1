#pragma strict

var daggerBody				: GameObject;
var daggerLeftHand			: GameObject;
var daggerRightHand			: GameObject;

var particleGeneric1		: GameObject;
var positionGeneric1		: GameObject;

var particlesGeneric2		: GameObject[];
var lightsGeneric2			: GameObject[];

var particlesGeneric3		: GameObject[];
var lightsGeneric3			: GameObject[];

var particleBarbarian1		: GameObject;
var positionBarbarian1		: GameObject;

function TakeDaggerOut(hand : String){
	daggerBody.SetActive(false);
	if (hand == "Left")
	{
		daggerLeftHand.SetActive(true);
		daggerRightHand.SetActive(false);
	}
	else if (hand == "Right")
	{
		daggerLeftHand.SetActive(false);
		daggerRightHand.SetActive(true);
	}
}

function PutBackDagger(hand : String){
	daggerBody.SetActive(true);
	daggerRightHand.SetActive(false);
	daggerLeftHand.SetActive(false);
}

function StartCastGeneric1(){
	InvokeRepeating("CastGeneric1", 0.0, 1.5);
}

function StopCastGeneric1(){
	CancelInvoke("CastGeneric1");
}

function CastGeneric1(){
	var newSpell	= Instantiate(particleGeneric1, positionGeneric1.transform.position, positionGeneric1.transform.rotation);
	Destroy(newSpell, 5.0);
}

function StartCastGeneric2(){
	for (var l : int; l < lightsGeneric2.Length; l++){
		lightsGeneric2[l].SetActive(true);
	}
	for (var p : int; p < particlesGeneric2.Length; p++){
		particlesGeneric2[p].GetComponent(ParticleSystem).Play();
	}
}

function StopCastGeneric2(){
	for (var l : int; l < lightsGeneric2.Length; l++){
		lightsGeneric2[l].SetActive(false);
	}
	for (var p : int; p < particlesGeneric2.Length; p++){
		particlesGeneric2[p].GetComponent(ParticleSystem).Stop();
	}
}

function StartCastGeneric3(){
	for (var l : int; l < lightsGeneric3.Length; l++){
		lightsGeneric3[l].SetActive(true);
	}
	for (var p : int; p < particlesGeneric3.Length; p++){
		particlesGeneric3[p].GetComponent(ParticleSystem).Play();
	}
}

function StopCastGeneric3(){
	for (var l : int; l < lightsGeneric3.Length; l++){
		lightsGeneric3[l].SetActive(false);
	}
	for (var p : int; p < particlesGeneric3.Length; p++){
		particlesGeneric3[p].GetComponent(ParticleSystem).Stop();
	}
}

function StartCastBarbarian1(){
	InvokeRepeating("CastBarbarian1", 0.0, 0.7);
}

function StopCastBarbarian1(){
	CancelInvoke("CastBarbarian1");
}

function CastBarbarian1(){
	var newSpell	= Instantiate(particleBarbarian1, positionBarbarian1.transform.position, positionBarbarian1.transform.rotation);
	Destroy(newSpell, 5.0);
}
