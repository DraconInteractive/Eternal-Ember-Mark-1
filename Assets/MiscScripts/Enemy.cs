using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	Player player;
	EnemyCounter eC;
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

	public float speedMod = 1;

	public GameObject pickupTemplate;

	public bool dead;

	bool searchForPlayer, stunned;

	// Use this for initialization
	void Start () {
		EnemyStart ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!dead && !stunned) {
			EnemyUpdate ();
		}

	}

	public virtual void EnemyStart () {
		eC = EnemyCounter.eC;
		eC.allEnemies.Add (GetComponent<Enemy> ());
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody> ();

		player = Player.player;

		searchForPlayer = true;
	}

	public virtual void EnemyUpdate () {
		if (searchForPlayer) {
			PlayerDetection ();
		}

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
		currentSpeed *= speedMod;
		anim.SetFloat ("Speed", currentSpeed);

	}

	public void ProcSlow (float duration) {
		StartCoroutine (SlowEnemy (duration));
	}

	public void ProcStun (float duration) {
		if (!stunned) {
			StartCoroutine (StunEnemy (duration));
		}

	}

	IEnumerator SlowEnemy (float duration) {
		speedMod -= 0.5f;
		speedMod = Mathf.Clamp (speedMod, 0, 1);
		yield return new WaitForSeconds (duration);
		speedMod += 0.5f;
		speedMod = Mathf.Clamp (speedMod, 0, 1);
		yield break;
	}

	IEnumerator StunEnemy (float duration) {
		stunned = true;
		yield return new WaitForSeconds (duration);
		stunned = false;
		yield break;
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

	public void PlayerFound () {
		if (playerDetected) {
			return;
		}
			
		if (anim != null) {
			anim.SetTrigger ("Scream");
		}
		playerDetected = true;
//		DisableSFP ();
//		Invoke ("EnableSFP", 3);
	}

	public void ExternalDetection () {
		playerDetected = true;
		DisableSFP ();
		Invoke ("EnableSFP", 3);
	}

	void PlayerLost () {
		if (anim != null) {
			anim.SetTrigger ("PlayerLost");
		}
		playerDetected = false;
	}

	void DisableSFP () {
		searchForPlayer = false;
	}

	void EnableSFP () {
		searchForPlayer = true;
	}

	public void DetectPlayer () {
		PlayerFound ();

	}
	public virtual void OnDeath () {
		rb.isKinematic = true;
		GetComponent<Collider> ().enabled = false;
		dead = true;
	}

		
}
