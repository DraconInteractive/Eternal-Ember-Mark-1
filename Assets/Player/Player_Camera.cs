using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using DeepSky.Haze;

public class Player_Camera : MonoBehaviour {

	public static Player_Camera p_camera;

	Player player;

	public Vector3 camOffset;
	public Vector3 invOffset;

	public GameObject headPoint;

	Vector3 cameraVelocity;

	bool freeLookEnabled;
	public bool invActive, atTarget;

	float focusZOffset;

	public bool inControl = true;

//	public DS_HazeView camHaze;
	void Awake () {
		p_camera = GetComponent<Player_Camera> ();

//		camHaze = GetComponent<DS_HazeView> ();
//		GameObject g = GameObject.Find ("DS_HazeController");
//		if (g == null) {
//			camHaze.enabled = false;
//		} else {
//			camHaze.enabled = true;
//		}
	}

	// Use this for initialization
	void Start () {
		player = Player.player;
		focusZOffset = 0;
	}

	void Update () {
		if (inControl) {
			focusZOffset += Input.GetAxis ("Mouse ScrollWheel") * 2;
			focusZOffset = Mathf.Clamp (focusZOffset, -1, 1);
		}

	}

	// Update is called once per frame
	void FixedUpdate () {
		if (!inControl) {
			return;
		}

		CameraMovement (camOffset, headPoint.transform.position + headPoint.transform.forward * 3.0f, 0.05f);

	}

	void CameraMovement (Vector3 target, Vector3 lookTarget, float moveTime) {
		target.z -= focusZOffset;
//		print (focusZOffset.ToString());
		Vector3 targetPosition = player.transform.position + (player.transform.right * target.x) + (player.transform.up * target.y) - (player.transform.forward * target.z);
		transform.position = Vector3.SmoothDamp (transform.position, targetPosition, ref cameraVelocity, moveTime);
		transform.LookAt (lookTarget);
	}

	public IEnumerator BeginNPCInteraction (NPC npc) {
		float f = 0;
		while (transform.position != npc.camTarget.position) {
			transform.position = Vector3.SmoothDamp (transform.position, npc.camTarget.position, ref cameraVelocity, 0.5f);
			transform.LookAt (npc.headTarget.position);
			f += Time.deltaTime;
			if (f > 3) {
				yield break;
			}
			yield return null;
		}
		yield break;
	}

	public IEnumerator EndNPCInteraction (NPC npc) {
		yield break;
	}
}
