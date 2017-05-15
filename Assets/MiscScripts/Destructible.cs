using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour {

	public bool hasCutoutShader;
	public Renderer[] allRenderers;
	public bool destroyed;

	public float dissolveSpeedMult, dissolveJump;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter (Collision col) {
//		print ("Collided");
//		Destruct ();
	}

	public void Destruct () {
		if (destroyed) {
			return;
		}
		if (hasCutoutShader) {
			StartCoroutine (Dissolve(true, dissolveSpeedMult, true));
			destroyed = true;
		}
	}

	public IEnumerator Dissolve (bool state, float speedMult, bool destroyOnDissolve) {
//		Renderer r = GetComponent<Renderer> ();
//
//		r.material.SetFloat ("_Cutoff", dissolveJump);
//		for (float c = dissolveJump; c < 1; c += Time.deltaTime * dissolveSpeedMult) {
//			r.material.SetFloat ("_Cutoff", c);
//			yield return null;
//		}
		if (state) {
			for (float f = 0; f < 1; f += Time.deltaTime * speedMult) {
				foreach (Renderer r in allRenderers) {
					r.material.SetFloat ("_Cutoff", f);
				}
			}

			if (destroyOnDissolve) {
				Destroy (this.gameObject, 0.1f);
			}
		} else {
			for (float f = 1; f > 0; f -= Time.deltaTime * speedMult) {
				foreach (Renderer r in allRenderers) {
					r.material.SetFloat ("_Cutoff", f);
				}
			}
		}
			
	
		yield break; 
	}




}
