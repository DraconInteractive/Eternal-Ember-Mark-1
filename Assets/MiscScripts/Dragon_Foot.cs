using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_Foot : MonoBehaviour {
	public bool collided;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter (Collision col) {
		print (gameObject.name + " foot hit");
		collided = true;
	}
}
