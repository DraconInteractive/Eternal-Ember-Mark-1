using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_Attached : MonoBehaviour {

	public int spellNum;

	public bool attached;
	public float destroyTime;
	public int kFivePrompt;

	public float spellDamage;
	// Use this for initialization
	void Start () {
		var tm = GetComponentInChildren<RFX4_TransformMotion>(true);

		if (tm!=null) {
			tm.CollisionEnter += Tm_CollisionEnter;
		} else {
			var tmt = GetComponentInChildren<RFX4_RaycastCollision> (true);

			if (tmt != null) {
				tmt.onRaycastCollide += Tmt_OnRaycastCollide;
			}
		} 

		if (!attached) {
			if (spellNum == 1 || spellNum == 2 || spellNum == 4) {
				Destroy (this.gameObject, destroyTime);
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (spellNum == 5 && !attached) {
			switch (kFivePrompt) 
			{
			case 1:
				if (Input.GetKeyUp(KeyCode.Alpha1)) {
					ParticleSystem[] ps = GetComponentsInChildren<ParticleSystem> ();
					foreach (ParticleSystem p in ps) {
						p.Stop ();
					}
					Destroy (this.gameObject, destroyTime);
				}
				break;
			case 2:
				if (Input.GetKeyUp(KeyCode.Alpha2)) {
					ParticleSystem[] ps = GetComponentsInChildren<ParticleSystem> ();
					foreach (ParticleSystem p in ps) {
						p.Stop ();
					}
					Destroy (this.gameObject, destroyTime);
				}
				break;
			case 3:
				if (Input.GetKeyUp(KeyCode.Alpha3)) {
					ParticleSystem[] ps = GetComponentsInChildren<ParticleSystem> ();
					foreach (ParticleSystem p in ps) {
						p.Stop ();
					}
					Destroy (this.gameObject, destroyTime);
				}
				break;
			case 4:
				if (Input.GetKeyUp(KeyCode.Alpha4)) {
					ParticleSystem[] ps = GetComponentsInChildren<ParticleSystem> ();
					foreach (ParticleSystem p in ps) {
						p.Stop ();
					}
					Destroy (this.gameObject, destroyTime);
				}
				break;
			}
//			if (Input.GetKeyUp(KeyCode.Alpha5)) {
				
//			}
		}
	}

	//	void Start ()
	//	{
	//		var tm = GetComponentInChildren<RFX4_TransformMotion>(true);
	//		if (tm!=null) tm.CollisionEnter += Tm_CollisionEnter;
	//	}
	//
	private void Tm_CollisionEnter(object sender, RFX4_TransformMotion.RFX4_CollisionInfo e)
	{
//		Debug.Log(e.Hit.transform.name); //will print collided object name to the console.

		Destructible dObj = e.Hit.transform.gameObject.GetComponent<Destructible> ();
		if (dObj != null) {
			dObj.Destruct ();
		}

		Collider[] hits = Physics.OverlapSphere (e.Hit.point, 1);
		foreach (Collider c in hits) {
			Health h = c.GetComponent<Health> ();
			if (h != null) {
				
				if (spellNum == 4) {
					h.ProcPoison (spellDamage * Time.deltaTime, 3, Health.poisonType.Fire, Player.player.gameObject);
				} else {
					h.Damage (spellDamage, Player.player.gameObject);
				}


			}
		}
	}

	private void Tmt_OnRaycastCollide (RaycastHit hit) {
		Destructible dObj = hit.transform.gameObject.GetComponent<Destructible> ();
		if (dObj != null) {
			dObj.Destruct ();
		}

		Collider[] hits = Physics.OverlapSphere (hit.point, 3);
		foreach (Collider c in hits) {
			Health h = c.GetComponent<Health> ();
			if (h != null) {
				h.Damage (spellDamage, Player.player.gameObject);
			}
		}
	}
}
