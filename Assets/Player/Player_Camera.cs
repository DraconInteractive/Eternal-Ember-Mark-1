﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Camera : MonoBehaviour {

	public static Player_Camera p_camera;

	Player player;

	public Vector3 camOffset;
	public Vector3 invOffset;

	public GameObject headPoint;

	Vector3 cameraVelocity;

	bool freeLookEnabled;
	public bool invActive, atTarget;

	void Awake () {
		p_camera = GetComponent<Player_Camera> ();
	}

	// Use this for initialization
	void Start () {
		player = Player.player;
	}

//	void Update () {
//		if (Input.GetMouseButtonDown(1)) {
//			freeLookEnabled = true;
//		} else {
//			freeLookEnabled = false;
//		}
//	}

	// Update is called once per frame
	void FixedUpdate () {
		if (invActive) {
//			CameraMovement (invOffset, headPoint.transform.position, 1.0f);
		} else {
			CameraMovement (camOffset, headPoint.transform.position + headPoint.transform.forward * 3.0f, 0.05f);
		}

//		if (!freeLookEnabled) {
//			
//		}
	}

	void CameraMovement (Vector3 target, Vector3 lookTarget, float moveTime) {
//		transform.position = Vector3.SmoothDamp (transform.position, player.transform.position + (player.transform.right * target.x) + (player.transform.up * target.y) - (player.transform.forward * target.z), ref cameraVelocity, moveTime);
//
//		float targetDist = Vector3.Distance (transform.position, player.transform.position + (player.transform.right * target.x) + (player.transform.up * target.y) - (player.transform.forward * target.z));
//		if (targetDist < 0.1f) {
//			atTarget = true;
//		} else {
//			atTarget = false;
//		}
//
//		if (invActive) {
//			if (targetDist < 1.5f) {
////				transform.LookAt (lookTarget);
//				Vector3 targetDir = lookTarget - transform.position;
//				transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetDir, Vector3.up), 45 * Time.deltaTime);
//			}
//		} else {
//			transform.LookAt (lookTarget);
//		}

		transform.position = Vector3.SmoothDamp (transform.position, player.transform.position + (player.transform.right * target.x) + (player.transform.up * target.y) - (player.transform.forward * target.z), ref cameraVelocity, moveTime);
		transform.LookAt (lookTarget);
	}
		
	public void INVInitiator () {
		StartCoroutine(MoveToINVPos(invOffset, headPoint.transform.position - player.transform.up * 0.5f, 0.01f));	

	}

	IEnumerator MoveToINVPos (Vector3 target, Vector3 lookTarget, float moveTime) {

		Vector3 targetDir = Vector3.one;

		float targetDist = 1;
		float targetAngle = 2;

		float minTime = 0.02f;
		while (minTime > 0) {
			
			if (invActive != true) {
				print ("instant break");
				yield break;
			}

			targetDir = lookTarget - transform.position;

//			targetDist = Vector3.Distance (transform.position, player.transform.position + (player.transform.right * target.x) + (player.transform.up * target.y) - (player.transform.forward * target.z));
//			targetAngle = Quaternion.Angle (transform.rotation, Quaternion.LookRotation (targetDir, Vector3.up));


			transform.position = Vector3.SmoothDamp (transform.position, player.transform.position + (player.transform.right * target.x) + (player.transform.up * target.y) - (player.transform.forward * target.z), ref cameraVelocity, moveTime);
//			if (targetDist < 3.0f) {
//				transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetDir, Vector3.up), 45 * Time.deltaTime);
//			}
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetDir, Vector3.up), 360);

			minTime -= Time.deltaTime;
			yield return null;
		}

		atTarget = true;

		yield break;
	}
}
