using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Progression : MonoBehaviour {

	public static Player_Progression p_prog;

	public string[] points;

	public Dictionary<string, bool> progression  = new Dictionary<string, bool>();

	void Awake () {
		p_prog = GetComponent<Player_Progression> ();
	}
	void Start () {
		foreach (string s in points) {
			progression.Add (s, false);
		}

//		UpdateProgression ("dragonFound", true);
	}

	public void UpdateProgression (string s, bool b) {
		progression [s] = b;
		print (s + " : " + progression [s].ToString ());
	}


}
