using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingText : MonoBehaviour {

	Text textC;

	public string [] sPool;
	void Awake () {
		textC = GetComponent<Text> ();
	}
	// Use this for initialization
	void Start () {
		ChangeText ();
	}

	void ChangeText () {
		string c = textC.text;

		int r = Random.Range (0, sPool.Length);

		if (sPool.Length > 1) {
			string t = sPool [r];
			if (t != c) {
				textC.text = t;
			}
		}

		Invoke ("ChangeText", 5);
	}
}
