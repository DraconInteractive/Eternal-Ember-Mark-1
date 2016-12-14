#pragma strict

var maleObj				: GameObject;
var femaleObj			: GameObject;
var maleAnim			: Animator;
var femaleAnim			: Animator;

var hairMatA			: Material;
var hairMatB			: Material;
var hairMatBuzzed		: Material;

var hairColorA			: Color[];
var hairColorB			: Color[];

var hair				: SkinnedMeshRenderer[];
var hairBuzzed			: SkinnedMeshRenderer;

var hairMaterialsA		: Material[];
var hairMaterialsB		: Material[];
var hairMaterialsZ		: Material[];

var eyeMat				: ProceduralMaterial;
var eyeColorSliders		: UI.Slider[];
var eyeColorImage		: UI.Image;

var bowLeft				: GameObject[];
var crossbowLeft		: GameObject[];
var bowRight			: GameObject[];
var crossbowRight		: GameObject[];

function Start () {
	maleAnim	= maleObj.GetComponent(Animator);
	femaleAnim	= femaleObj.GetComponent(Animator);
}

function Locomotion(newValue : float){
	maleAnim.SetFloat("locomotion", newValue);
	femaleAnim.SetFloat("locomotion", newValue);
}

function AnimationTrigger(newValue : String){
	maleAnim.SetTrigger(newValue);
	femaleAnim.SetTrigger(newValue);
}

// 0, 25, 122 = default
function SetEyes(){
	var newColor	: Color	= Color(eyeColorSliders[0].value, eyeColorSliders[1].value, eyeColorSliders[2].value, 1);
	eyeMat.SetProceduralColor("EyeColor", newColor);
	eyeMat.RebuildTextures();
	eyeColorImage.color	= newColor;

	print ("newColor: " + newColor);
}

function SetHair(newValue : int){
	for (var i : int; i < hair.Length; i++){
		var materials	: Material[]	= new Material[2];
		materials[0]	= hairMaterialsA[newValue];
		materials[1]	= hairMaterialsB[newValue];
		hair[i].materials	= materials;
	}
	var buzzMaterials	: Material[]	= new Material[1];
	buzzMaterials[0]	= hairMaterialsZ[newValue];
	hairBuzzed.materials	= buzzMaterials;
}

function SetTimescale(newValue : float){
	Time.timeScale	= newValue;
}

function SetLeftHandGripWeight(newValue : float){
	maleAnim.SetLayerWeight(2, newValue);
	femaleAnim.SetLayerWeight(2, newValue);
}

function SetRightHandGripWeight(newValue : float){
	maleAnim.SetLayerWeight(3, newValue);
	femaleAnim.SetLayerWeight(3, newValue);
}

function BowLeft(animationStep : String){
	print ("BowLeft: " + animationStep);
	for (var i : int; i < bowLeft.Length; i++){
		bowLeft[i].SetActive(true);
		bowLeft[i].GetComponent(Animator).SetTrigger(animationStep);
	}
}

function BowRight(animationStep : String){
	print ("BowRight: " + animationStep);
	for (var i : int; i < bowRight.Length; i++){
		bowRight[i].SetActive(true);
		bowRight[i].GetComponent(Animator).SetTrigger(animationStep);
	}
}

function CrossbowLeft(animationStep : String){
	SetLeftHandGripWeight(0.0);
	SetRightHandGripWeight(0.0);
	print ("CrossbowLeft: " + animationStep);
	for (var i : int; i < crossbowLeft.Length; i++){
		crossbowLeft[i].SetActive(true);
		crossbowLeft[i].GetComponent(Animator).SetTrigger(animationStep);
	}
}

function CrossbowRight(animationStep : String){
	print ("CrossbowRight: " + animationStep);
	for (var i : int; i < crossbowRight.Length; i++){
		if (animationStep == 1)
		crossbowRight[i].SetActive(true);
		crossbowRight[i].GetComponent(Animator).SetTrigger(animationStep);
	}
}

function TurnOffBows(){
	for (var i : int; i < crossbowLeft.Length; i++){
		crossbowLeft[i].SetActive(false);
		crossbowRight[i].SetActive(false);
		SetLeftHandGripWeight(1.0);
		SetRightHandGripWeight(1.0);
	}
	for (var i2 : int; i2 < bowLeft.Length; i2++){
		bowLeft[i2].SetActive(false);
		bowRight[i2].SetActive(false);
	}
}