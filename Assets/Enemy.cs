using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	Player player;

	public Animator anim;
	Rigidbody rb;
	bool hasAnim;

	public bool debug_isAggressive;

	public float detectionRange, attackingRange;
	public float speed;
	public bool playerDetected;
	public GameObject eyes;
	public float attackTimer, attackTimerTarget;
//	Vector3 aiVelocity;
	float currentSpeed;

	public GameObject pickupTemplate;
	public List<Item> possibleDrops;

	public bool dead;

	// Use this for initialization
	void Start () {
		EnemyStart ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!dead) {
			EnemyUpdate ();
		}

	}

	public virtual void EnemyStart () {
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody> ();

		player = Player.player;
	}

	public virtual void EnemyUpdate () {
		PlayerDetection ();
		EnemyMovement ();
		if (debug_isAggressive) {
			Combat ();
		}

	}
		
	public virtual void Combat () {
		if (attackTimer < attackTimerTarget) {
			attackTimer += Time.deltaTime;
		}
			
		if (attackTimer >= attackTimerTarget) {
			Attack ();
		}
	}

	public virtual void Attack () {
		
		anim.SetInteger("attackIndex",(int)Random.Range (1.0f, 4.0f));
		anim.SetTrigger ("Attack");
		attackTimer = 0;
	}

	void PlayerDetection () {
		//Get Distance To Player
		//See if player is in sight angle
		//See if there is any sight obstacles between the player and the enemies
		Vector3 pR = player.transform.position + player.transform.up * 1.5f;
		if (CheckPlayerDistance(pR, detectionRange)) {
			if (CheckPlayerSightAngle(pR)) {
				if (CheckPlayerUnobstructedSight (pR)){
					if (!playerDetected) {
						PlayerFound ();
					}
					playerDetected = true;
				} else {
					if (playerDetected) {
						PlayerLost ();
					}
					playerDetected = false;
				}
			} else {
				if (playerDetected) {
					PlayerLost ();
				}
				playerDetected = false;
			}
		} else {
			if (playerDetected)
				PlayerLost ();
			playerDetected = false;
		}
	}

	void EnemyMovement () {
		bool moving;
		if (playerDetected) {
			Vector3 pR = player.transform.position + Vector3.up * 1.5f;
			if (!CheckPlayerDistance(pR, attackingRange)) {
//				rb.MovePosition (Vector3.SmoothDamp(transform.position, player.transform.position, ref aiVelocity, 1));
				moving = true;
				Quaternion targetRotation = Quaternion.LookRotation (player.transform.position - transform.position, Vector3.up);
				transform.rotation = Quaternion.RotateTowards (transform.rotation, targetRotation, 45);
			} else {
				moving = false;
			}
		} else {
			moving = false;
		}

		if (moving) {
			currentSpeed += Time.deltaTime;
		} else {
			currentSpeed -= Time.deltaTime;
		}

		currentSpeed = Mathf.Clamp01(currentSpeed);
		anim.SetFloat ("Speed", currentSpeed);

	}

	bool CheckPlayerDistance (Vector3 playerR, float distance) {
		float playerDist = Vector3.Distance (playerR, eyes.transform.position);

		if (playerDist < distance) {
			return true;
		}

		return false;
	}

	bool CheckPlayerSightAngle (Vector3 playerR) {
		Quaternion rotToPlayer = Quaternion.LookRotation (playerR - eyes.transform.position, Vector3.up);
		float sightAngle = Quaternion.Angle (transform.rotation, rotToPlayer);
		if (sightAngle < 65) {
			return true;
		} else {
			return false;
		}
	}

	bool CheckPlayerUnobstructedSight (Vector3 playerR) {
		Ray ray = new Ray (eyes.transform.position, playerR - eyes.transform.position);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
			if (hit.collider.tag != "Player" && hit.collider.tag != "Enemy") {
				return false;
			}
		}
		return true;
	}

	void PlayerFound () {
		if (anim != null) {
			anim.SetTrigger ("Scream");
		}
	}

	void PlayerLost () {
		if (anim != null) {
			anim.SetTrigger ("PlayerLost");
		}
	}

	public virtual void OnDeath () {
		rb.isKinematic = true;
		GetComponent<Collider> ().enabled = false;
		dead = true;
		DropItem ();
	}

	public void DropItem () {
		GameObject pickup = Instantiate (pickupTemplate, transform.position, Quaternion.identity) as GameObject;
		PickUp p = pickup.GetComponent<PickUp> ();
		int i = Random.Range (0, possibleDrops.Count);
		p.pickupItem = possibleDrops [i];
	}
		
}
