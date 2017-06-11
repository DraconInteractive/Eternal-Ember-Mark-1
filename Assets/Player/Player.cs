using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public static Player player;

	[HideInInspector]
	public Player_SpellControl p_SpellControl;

	Renderer[] playerFigureRenderers;

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
	int sOneIndex;

	Coroutine stealthRoutine;
	bool stealthed;

	[HideInInspector]
	public RuneControl rune_control;
	void Awake () {
		player = GetComponent<Player> ();

		rb = GetComponent<Rigidbody> ();
		anim = GetComponentInChildren<Animator> ();
	}

	void Start () {
		DontDestroyOnLoad (transform.parent.gameObject);

		p_UI = Player_UI.p_UI;
		pc_controller = PossessionController.pc_controller;
		SetInCombat (false);
		p_Movement = Player_Movement.p_movement;
		currentHealth = maxHealth;
		p_UI.UpdateHealthSlider (currentHealth, maxHealth);
		p_SpellControl = Player_SpellControl.spellControl;
		pc_controller = PossessionController.pc_controller;
	
		playerFigureRenderers = p_SpellControl.gameObject.GetComponentsInChildren<Renderer> ();

//		stealthRoutine = StartCoroutine (ToggleStealth (false, 1));

		rune_control = RuneControl.rune_control;
	}
	// Use this for initialization
	void Update () {
		P_Input ();
	}

	void P_Input () {
		if (!p_Movement.canMove || !possessed) {
			return;
		}

		#region attackinput
		if (Input.GetMouseButtonDown(0)) {
			if (attackRoutine == null) {
				attackRoutine = StartCoroutine (Attack (0, 0));
			}
		}

		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			if (attackRoutine == null) {
				attackRoutine = StartCoroutine (Attack (p_SpellControl.spellOne, 1));
			}
		}

		if (Input.GetKeyDown(KeyCode.Alpha2)) {
			if (attackRoutine == null) {
				attackRoutine = StartCoroutine (Attack (p_SpellControl.spellTwo, 2));
			}
		}

		if (Input.GetKeyDown(KeyCode.Alpha3)) {
			if (attackRoutine == null) {
				attackRoutine = StartCoroutine (Attack (p_SpellControl.spellThree, 3));
			}
		}

		if (Input.GetKeyDown(KeyCode.Alpha4)) {
			if (attackRoutine == null) {
				attackRoutine = StartCoroutine (Attack (p_SpellControl.spellFour, 4));
			}
		}
		#endregion
//		if (Input.GetKeyDown(KeyCode.Alpha5)) {
//			if (attackRoutine == null) {
//				attackRoutine = StartCoroutine (Attack (5));
//			}
//		}

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

		if (Input.GetKeyDown(KeyCode.F)) {

			Ray ray = new Ray (transform.position + transform.up, transform.forward);
			RaycastHit[] hits = Physics.RaycastAll (ray, 5);
			foreach (RaycastHit hit in hits) {
				NPC npc = hit.transform.gameObject.GetComponent<NPC> ();
				if (npc != null) {
					Interact (npc);
					break;
				}
			}
		}
			
		if (Input.GetKeyDown(KeyCode.Space)) {
			if (stealthRoutine == null) {
//				stealthRoutine = StartCoroutine (ToggleStealth (!stealthed, 1));
			}
		}
	}

	public void Interact (NPC npc) {
		npc.Interact ();
		anim.SetTrigger ("Interact");
	}

	#region combat
	IEnumerator Attack (int attackType, int keyPrompt) { 
		
//		float timer = Time.realtimeSinceStartup;

		if (!inCombat) {
			SetInCombat (true);
		}

		if (playerWeapon != null) {
//			playerDamage = playerWeapon.thisItem.itemPower;
		} else {
			if (attackType == 0) {
				playerDamage = playerUnarmedDamage;
			} else if (attackType == 1) {
				playerDamage = 0;
			}

		}
		p_Movement.canMove = false;

		if (attackType == 0) {
			Collider[] c = Physics.OverlapCapsule (transform.position + transform.forward, transform.position + transform.forward + transform.up * 1.5f, 1);
			anim.SetTrigger ("Attack");
			yield return new WaitForSeconds (0.25f);
			for (int i = 0; i < c.Length; i++) {
				if (c[i].tag == "Enemy") {
					c [i].gameObject.GetComponent<Health> ().Damage (playerDamage, this.gameObject);
//					rune_control.onPlayerAttack (c [i].gameObject);
					rune_control.ActivateAttackRunes (c [i].gameObject);
				}

			}
		} else if (attackType == 1) {
			anim.SetInteger ("SpellIndex", 1);
			anim.SetTrigger ("CastSpell");
//			yield return new WaitForSeconds (0.25f);
		} else if (attackType == 2) {
			anim.SetInteger ("SpellIndex", 2);
			anim.SetTrigger ("CastSpell");
			yield return new WaitForSeconds (6.5f);
		} else if (attackType == 3) {
			anim.SetInteger ("SpellIndex", 3);
			anim.SetTrigger ("CastSpell");
			yield return new WaitForSeconds (4.5f);
		} else if (attackType == 4) {
			anim.SetInteger ("SpellIndex", 4);
			anim.SetTrigger ("CastSpell");
			yield return new WaitForSeconds (2.5f);
		} else if (attackType == 5) {
			p_SpellControl.sFive_k_prompt = keyPrompt;
			anim.SetInteger ("SpellIndex", 5);
			anim.SetTrigger ("CastSpell");
			bool endSpell = false;
			while (endSpell == false) {
				switch (keyPrompt) {
				case 1:
					if (Input.GetKeyUp(KeyCode.Alpha1)) {
						endSpell = true;
					}
					break;
				case 2:
					if (Input.GetKeyUp(KeyCode.Alpha2)) {
						endSpell = true;
					}
					break;
				case 3:
					if (Input.GetKeyUp(KeyCode.Alpha3)) {
						endSpell = true;
					}
					break;
				case 4:
					if (Input.GetKeyUp(KeyCode.Alpha4)) {
						endSpell = true;
					}
					break;
				}
				yield return null;
			}
			anim.SetTrigger ("EndSpell");

			yield return new WaitForSeconds (1);
			p_SpellControl.DeactivateEffect (5);
		}
		p_Movement.canMove = true;
//		float updatedTimer = timer - Time.realtimeSinceStartup;
//		print ("Attack Done");

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
				
		} else {
			anim.SetBool ("Armed", false);
		}


	}
	#endregion

	#region pickup
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
	#endregion

	#region helperFunctions
	public void ToggleCursor (bool state) {
		Cursor.visible = state;
		if (state) {
			Cursor.lockState = CursorLockMode.None;
		} else {
			Cursor.lockState = CursorLockMode.Locked;
		}
	}
	#endregion

	#region sneaking

	IEnumerator ToggleStealth (bool state, float speedMult) {
		if (state) {
			for (float f = 0; f < 1; f += Time.deltaTime * speedMult) {
				foreach (Renderer r in playerFigureRenderers) {
					r.material.SetFloat ("_Cutoff", f);
				}
			}
		} else {
			for (float f = 1; f > 0; f -= Time.deltaTime * speedMult) {
				foreach (Renderer r in playerFigureRenderers) {
					r.material.SetFloat ("_Cutoff", f);
				}
			}
		}

		stealthed = state;

		stealthRoutine = null;

		yield break;

	}

	#endregion
}


