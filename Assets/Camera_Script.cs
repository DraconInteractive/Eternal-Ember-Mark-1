using UnityEngine;
using System.Collections;

public class Camera_Script : MonoBehaviour {
	Player_Script player;

	GameObject target;

	public float playerXO, playerYO, playerZO;

	// Use this for initialization
	void Start () {
		player = Player_Script.player;
		SetTarget (player.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		CameraMovement ();
	}

	void CameraMovement () {
		Vector3 targetPosition = target.transform.position - target.transform.forward * playerZO + target.transform.up * playerYO + target.transform.right * playerXO;
		transform.position = Vector3.MoveTowards (transform.position, targetPosition, 0.1f);
		transform.LookAt (target.transform.position + target.transform.forward * 1 + target.transform.up * 1);
	}

	void SetTarget(GameObject go) {
		target = go;
	}
}
