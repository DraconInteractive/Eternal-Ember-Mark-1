using System.Collections;
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
			CameraMovement (invOffset, headPoint.transform.position, 1.0f);
		} else {
			CameraMovement (camOffset, headPoint.transform.position + headPoint.transform.forward * 3.0f, 0.05f);
		}

//		if (!freeLookEnabled) {
//			
//		}
	}

	void CameraMovement (Vector3 target, Vector3 lookTarget, float moveTime) {

		transform.position = Vector3.SmoothDamp (transform.position, player.transform.position + (player.transform.right * target.x) + (player.transform.up * target.y) - (player.transform.forward * target.z), ref cameraVelocity, moveTime);
		transform.LookAt (lookTarget);

		if (Vector3.Distance(transform.position, player.transform.position + (player.transform.right * target.x) + (player.transform.up * target.y) - (player.transform.forward * target.z)) < 0.1f) {
			atTarget = true;
		} else {
			atTarget = false;
		}
	}
}
