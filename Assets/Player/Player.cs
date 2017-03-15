using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public static Player player;
	Player_Movement p_Movement;
	[HideInInspector]
	public Rigidbody rb;

	public float movementSpeed;
	Vector3 playerVelocity;
	[HideInInspector]
	public Animator anim;

	public GameObject leftHandAttach, rightHandAttach, backHolster;
	public Weapon playerWeapon;

	public bool inCombat;
	Coroutine attackRoutine;
	void Awake () {
		player = GetComponent<Player> ();
		rb = GetComponent<Rigidbody> ();
		anim = GetComponentInChildren<Animator> ();
	}

	void Start () {
		SetInCombat (false);
		p_Movement = Player_Movement.p_movement;
	}
	// Use this for initialization
	void Update () {
		P_Input ();
	}

	void P_Input () {
		if (Input.GetMouseButtonDown(0)) {
			if (attackRoutine == null) {
				attackRoutine = StartCoroutine (Attack ());
			}
		}

		if (Input.GetKeyDown(KeyCode.R)) {
			SetInCombat (!inCombat);
		}

		if (Input.GetKeyDown(KeyCode.E)) {
			if (inCombat) {
				
			} else {
				InitPickUpItem ();
			}

		}
	}

	IEnumerator Attack () {
		if (!inCombat) {
			SetInCombat (true);
		}
		Collider[] c = Physics.OverlapCapsule (transform.position + transform.forward, transform.position + transform.forward + transform.up * 1.5f, 1);
		anim.SetTrigger ("Attack");
		yield return new WaitForSeconds (0.25f);
		for (int i = 0; i < c.Length; i++) {
			if (c[i].tag == "Enemy") {
				c [i].gameObject.GetComponent<Enemy> ().Damage ();
			}
		}
//		if (playerWeapon != null) {
//			playerWeapon.attacking = true;
//		}
//
//		anim.SetTrigger ("Attack");
//		yield return new WaitForSeconds (0.25f);
//		if (playerWeapon != null) {
//			playerWeapon.attacking = false;
//		}

		attackRoutine = null;
		yield break;
	}

	public void SetInCombat (bool state) {
		inCombat = state;
		anim.SetBool ("inCombat", state);

		if (playerWeapon != null) {
			if (state) {
				playerWeapon.UnHolster ();
			} else {
				playerWeapon.Holster ();
			}
		}
	}
		
	void InitPickUpItem () {
//		Collider[] c = Physics.OverlapSphere (transform.position + transform.forward * 1 + Vector3.up * 0.1f, 1);
		Collider[] c = Physics.OverlapCapsule (transform.position + transform.forward, transform.position + transform.forward + transform.up, 1);

		foreach (Collider co in c) {
			if (co.tag == "Interactable") {
				anim.SetTrigger ("PickupItem");
				StartCoroutine (FTPickUpItem (co.gameObject));
				break;
			}
		}
	}

	IEnumerator FTPickUpItem (GameObject g) {
		p_Movement.canMove = false;
		yield return new WaitForSeconds (0.8f);
		Interactable inter = g.GetComponent<Interactable> ();
		if (inter != null) {
			inter.Interact ();
		}
		p_Movement.canMove = true;
		yield break;
	}
}
