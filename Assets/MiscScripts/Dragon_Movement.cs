using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_Movement : MonoBehaviour {

	Player player;
	public float groundSpeed/*, flightSpeed*/, rotationSpeed;
	public Vector3 playerVelocity;

	Rigidbody rb;
	Animator anim;

	Dragon dragon;

	public bool canMove, sprinting;

	float pitch, yaw;

	Coroutine homeRoutine;
	void Start () {
		dragon = Dragon.dragon;
		player = Player.player;
		rb = dragon.rb;
		anim = dragon.anim;
		canMove = true;

		GetComponent<Dragon_Movement> ().enabled = false;
	}

	void Update () {
		if (canMove) {
			anim.SetFloat ("speed", Input.GetAxis ("Vertical"));
		}


		if (Input.GetKeyDown(KeyCode.LeftShift)) {
			sprinting = true;
		}

		if (Input.GetKeyUp(KeyCode.LeftShift)) {
			sprinting = false;
		}

		if (Input.GetKeyDown(KeyCode.H)) {
			if (homeRoutine != null) {
				StopCoroutine (homeRoutine);
				homeRoutine = null;
				canMove = true;
			} else {
				homeRoutine = StartCoroutine (ReturnToPlayer ());
			}
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (canMove) {
			Movement ();
		}
	}
		
	void Movement () {
		Vector3 inputVelocity = new Vector3 (0, Input.GetAxis("Height"), Input.GetAxis ("Vertical"));
		float mod = 1;
		if (sprinting) {
			mod = 1.5f;
		}
		inputVelocity *= groundSpeed * mod * Time.fixedDeltaTime;

		if (dragon.flying) {
			inputVelocity *= 100;
		}
		//		rb.MovePosition (transform.position + inputVelocity * movementSpeed * Time.fixedDeltaTime);
		if (inputVelocity.z > 0) {
//			rb.MovePosition (transform.position + (transform.forward * inputVelocity.z) + (transform.right * inputVelocity.x));
			rb.MovePosition (Vector3.SmoothDamp(transform.position, transform.position + (transform.forward * inputVelocity.z) + (transform.right * inputVelocity.x), ref playerVelocity, 0.5f));
		} else {
//			rb.MovePosition (transform.position + (transform.forward * inputVelocity.z * 0.25f) + (transform.right * inputVelocity.x));
			rb.MovePosition (Vector3.SmoothDamp(transform.position, transform.position + (transform.forward * inputVelocity.z * 0.25f) + (transform.right * inputVelocity.x), ref playerVelocity, 0.5f));
		}



		if (dragon.flying) {
//			rb.MovePosition (transform.position + transform.up * inputVelocity.y);
			rb.MovePosition (Vector3.SmoothDamp (transform.position, transform.position + transform.up * inputVelocity.y, ref playerVelocity, 0.5f));

		}

//		rb.MoveRotation (transform.rotation * Quaternion.Euler (new Vector3 (0, Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime, 0)));

		pitch += Input.GetAxis ("Mouse Y") * rotationSpeed * Time.deltaTime;
		pitch = Mathf.Clamp (pitch, -90, 90);

		float yawInput = Input.GetAxis ("Mouse X") + Input.GetAxis ("Horizontal");
		yawInput = Mathf.Clamp (yawInput, -1, 1);
		yaw += yawInput * rotationSpeed * Time.deltaTime;

		if (dragon.flying) {
			transform.localEulerAngles = new Vector3 (-pitch, yaw, transform.localEulerAngles.z);
		} else {
			transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, yaw, transform.localEulerAngles.z);
		}
	}
		
	IEnumerator ReturnToPlayer () {
		canMove = false;
		if (!dragon.flying) {
			dragon.QuickFly ();
		}

		for (float a = 0; a < 2; a += Time.deltaTime) {
			transform.position += Vector3.up * 6 * Time.deltaTime;
			yield return null;
		}

		while (Vector3.Distance(transform.position, player.transform.position) > 50) {
			Vector3 targetVector = (new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z) - transform.position).normalized;
			Quaternion targetRot = Quaternion.LookRotation (targetVector, Vector3.up);
			transform.rotation = Quaternion.RotateTowards (transform.rotation, targetRot, 45 * Time.deltaTime);

			if (Quaternion.Angle(transform.rotation, targetRot) < 5) {
				transform.position += transform.forward * 16 * Time.deltaTime;
			}
			yield return null;
		}
		canMove = true;
		yield break;
	}
}
