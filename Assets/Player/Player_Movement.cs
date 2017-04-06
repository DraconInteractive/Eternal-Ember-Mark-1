using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour {
	public static Player_Movement p_movement;
	public float movementSpeed;
	Vector3 playerVelocity;

	Rigidbody rb;
	Animator anim;

	Player player;

	public bool canMove;

	void Awake () {
		p_movement = GetComponent<Player_Movement> ();
	}

	void Start () {
		player = Player.player;
		rb = player.rb;
		anim = player.anim;
		canMove = true;
	}

	void Update () {
		if (canMove) {
			anim.SetFloat ("speed", Input.GetAxis ("Vertical"));
		}

	}

	// Update is called once per frame
	void FixedUpdate () {
		if (canMove) {
			Movement ();
		}
	}

	void Movement () {
		Vector3 inputVelocity = new Vector3 (0, 0, Input.GetAxis ("Vertical"));

		inputVelocity *= movementSpeed * Time.fixedDeltaTime;

		//		rb.MovePosition (transform.position + inputVelocity * movementSpeed * Time.fixedDeltaTime);
		if (inputVelocity.z > 0) {
			rb.MovePosition (transform.position + (transform.forward * inputVelocity.z) + (transform.right * inputVelocity.x));
		} else {
			rb.MovePosition (transform.position + (transform.forward * inputVelocity.z * 0.25f) + (transform.right * inputVelocity.x));
		}

		rb.MoveRotation (transform.rotation * Quaternion.Euler (new Vector3 (0, Input.GetAxis("Mouse X"), 0)));
	}
}
