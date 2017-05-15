using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spell_Tooltip : MonoBehaviour {

	public Text spellName, spellDesc, spellType;
	// Use this for initialization
	void Start () {
		ClearInfo ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ClearInfo () {
		spellName.text = "";
		spellDesc.text = "";
		spellType.text = "";
	}

	public void SetInfo (string n, string d, string t) {
		spellName.text = n;
		spellDesc.text = d;
		spellType.text = t;
	}
}
