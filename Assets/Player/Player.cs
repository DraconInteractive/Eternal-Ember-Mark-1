using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public static Player player;
	[HideInInspector]
	public Player_Movement p_Movement;
	Player_UI p_UI;
	PossessionController pc_controller;
	[HideInInspector]
	public Rigidbody rb;

	public float movementSpeed;
	Vector3 playerVelocity;
	[HideInInspector]
	public Animator anim;

	public GameObject leftHandAttach, rightHandAttach;
	public Weapon playerWeapon;
	public Shield playerShield;

	public bool inCombat;
	Coroutine attackRoutine;

	public float currentHealth;
	public float maxHealth;

	public float playerDamage;
	public float playerUnarmedDamage;

	public GameObject playerCamera;

	public GameObject yyParticle;

	public int currentAttack;

	public bool blocking;

	public bool possessed;
	void Awake () {
		player = GetComponent<Player> ();

		rb = GetComponent<Rigidbody> ();
		anim = GetComponentInChildren<Animator> ();
	}

	void Start () {
		p_UI = Player_UI.p_UI;
		pc_controller = PossessionController.pc_controller;
		SetInCombat (false);
		p_Movement = Player_Movement.p_movement;
		currentHealth = maxHealth;
		p_UI.UpdateHealthSlider (currentHealth, maxHealth);
	}
	// Use this for initialization
	void Update () {
		P_Input ();
	}

	void P_Input () {
		if (!p_Movement.canMove || !possessed) {
			return;
		}

		if (Input.GetMouseButtonDown(0)) {
			if (attackRoutine == null) {
				attackRoutine = StartCoroutine (Attack ());
			}
		}

		if (Input.GetMouseButtonDown(1)) {
			blocking = true;
			anim.SetBool ("Blocking", true);
		}

		if (Input.GetMouseButtonUp(1)){
			blocking = false;
			anim.SetBool ("Blocking", false);
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
		if (playerWeapon != null) {
			playerDamage = playerWeapon.thisItem.itemPower;
		} else {
			playerDamage = playerUnarmedDamage;
		}
		Collider[] c = Physics.OverlapCapsule (transform.position + transform.forward, transform.position + transform.forward + transform.up * 1.5f, 1);
		anim.SetTrigger ("Attack");
		yield return new WaitForSeconds (0.25f);
		for (int i = 0; i < c.Length; i++) {
			if (c[i].tag == "Enemy") {
				c [i].gameObject.GetComponent<Health> ().Damage (playerDamage);
			}
		}

		attackRoutine = null;
		yield break;
	}

	public void SetInCombat (bool state) {
		inCombat = state;
		anim.SetBool ("inCombat", state);

		if (playerWeapon != null) {
			if (state) {
				anim.SetBool ("Armed", true);
				playerWeapon.UnHolster ();
			} else {
				playerWeapon.Holster ();
			}

			if (playerShield != null) {
				if (state) {
					playerShield.UnHolster ();
				} else {
					playerShield.Holster ();
				}
			}
		} else {
			anim.SetBool ("Armed", false);
		}


	}
		
	void InitPickUpItem () {
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

	public void ToggleCursor (bool state) {
		Cursor.visible = state;
		if (state) {
			Cursor.lockState = CursorLockMode.None;
		} else {
			Cursor.lockState = CursorLockMode.Locked;
		}
	}
		
}


