#pragma strict

var objectToWatch		: GameObject;
var objectToSwitch		: GameObject;

function Update(){
	objectToSwitch.SetActive(!objectToWatch.activeSelf);
}