using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour {

	public static Dragon dragon;
	Dragon_Movement d_movement;
	public GameObject dragonCamera, dragonHead;
	[HideInInspector]
	public Animator anim;
	[HideInInspector]
	public Rigidbody rb;

	[HideInInspector]
	public PossessionController pc_control;

	public float attackCooldown;
	public bool canAttack, aTwoActive, onGround, flying, landing;

	void Awake () {
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody> ();
		dragon = GetComponent<Dragon> ();
	}

	void Start () {
		d_movement = GetComponent<Dragon_Movement> ();
		ReactivateAttack ();

		pc_control = PossessionController.pc_controller;

		if (!pc_control.dragonInScene) {
			print ("Assigning dragon");
			pc_control.dragon = dragon;
			pc_control.dragonInScene = true;
			pc_control.dragonCamera = dragon.dragonCamera;
		}

		if (pc_control.currentHost == PossessionController.hostType.PLAYER) {
//			dragonCamera.SetActive (false);
			GetComponent<Dragon_Movement> ().enabled = false;
		}
	}

	void OnCollisionStay (Collision col) {
		if (col.gameObject.tag == "Ground") {
			onGround = true;
//			print ("h");

			if (landing) {
				Land ();
			}
		}
	}

//	void OnEnable () {
//		
//	}

//	// Use this for initialization
//	void Start () {
//		
//	}
//	
	// Update is called once per frame
	void Update () {
		if (d_movement.enabled == true) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				if (landing) {
					AbortDive ();
					return;
				}
				if (onGround && !flying && Input.GetAxis ("Vertical") == 0) {
					Fly ();
				} else if (flying && onGround) {
					Land ();
				} else if (flying && !onGround) {
					InitiateLand ();
				} else if (!onGround && !flying) {
					QuickFly ();
				}
			}

			if (Input.GetMouseButtonDown(0)) {
				Attack (1);
			}

			if (Input.GetMouseButtonDown(1)) {
				Attack (2);
			}
		}

	}

	void Attack (int aType) {
		if (canAttack) {
			switch (aType)
			{
			case 1:
				canAttack = false;
				anim.SetTrigger ("Attack");
				Invoke ("ReactivateAttack", attackCooldown);
				break;
			case 2:
				canAttack = false;
				anim.SetTrigger ("FireBreath");
				Invoke ("ReactivateAttack", attackCooldown);
				break;
			}

		}
	}
		
	void ReactivateAttack () {
		canAttack = true;
	}

	void FixedUpdate () {
		onGround = false;
	}

	public void Fly () {
//		Dragon_Movement d_movement = GetComponent<Dragon_Movement> ();
		d_movement.canMove = false;
		rb.isKinematic = true;
		flying = true;
		anim.SetTrigger ("Fly");
		StartCoroutine ("FlyInit");
	}

	public void QuickFly () {
		rb.isKinematic = true;
		flying = true;
		anim.SetTrigger ("Fly");
	}

	IEnumerator FlyInit () {
		
		yield return new WaitForSeconds (0.25f);
		for (float f = 0; f < 3; f += Time.deltaTime) {
			transform.position += (transform.up * 2 * Time.deltaTime);
			yield return null;
		}
		d_movement.canMove = true;
		yield break;

	}

	public void InitiateLand () {
		landing = true;
		rb.isKinematic = false;
		anim.SetTrigger ("Dive");
		anim.ResetTrigger ("Land");
		anim.ResetTrigger ("AbortDive");
	}

	public void AbortDive () {
		landing = false;
		rb.isKinematic = true;
		anim.SetTrigger ("AbortDive");
	}

	public void Land () {
//		rb.isKinematic = false;
		landing = false;
		flying = false;
		anim.SetTrigger ("Land");
		anim.ResetTrigger ("AbortDive");
		anim.ResetTrigger ("Dive");

		transform.up = Vector3.up;
	}

}
