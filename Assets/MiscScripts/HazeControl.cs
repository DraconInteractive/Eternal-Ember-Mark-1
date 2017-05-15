using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazeControl : MonoBehaviour {

	PossessionController posController;


	// Use this for initialization
	void Start () {
		posController = PossessionController.pc_controller;
	}
	
	// Update is called once per frame
	void Update () {
		if (posController.currentHost == PossessionController.hostType.PLAYER) {
			transform.position = posController.playerCamera.transform.position;
		} else if (posController.currentHost == PossessionController.hostType.DRAGON) {
			transform.position = posController.dragonCamera.transform.position;
		}
	}
}
