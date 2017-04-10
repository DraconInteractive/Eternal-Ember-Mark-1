#pragma strict

var turnOff				: UI.Toggle[];						// Array of Toggles to turn off.
private var thisToggle	: UI.Toggle;						// This toggle
private var isOn		: boolean			= false;		// Is this one on?

function Start () {
	thisToggle	= GetComponent.<UI.Toggle>();
}

function Update () {
	if (!isOn && thisToggle.isOn)				// If the toggle is on but our variable is false
		TurnOthersOff();						// Run this function
	else if (isOn && !thisToggle.isOn)			// If the toggle is not on, but our variable is true
		isOn	= false;						// Set our variable to false
}

function TurnOthersOff(){
	for (var i : int; i < turnOff.Length; i++){
		turnOff[i].isOn	= false;
	}
}