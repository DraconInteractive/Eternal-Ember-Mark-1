using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spell_Icon : MonoBehaviour {
	public GameObject toolTipPanel;
	Spell_Tooltip s_tooltip;
	public Spell spell;
	Image thisImage, learnedImage;

	public bool iconInteractable;

	void Awake () {
		thisImage = GetComponent<Image> ();
		learnedImage = transform.GetChild (0).GetComponent<Image> ();
	}
	// Use this for initialization
	void Start () {
		s_tooltip = toolTipPanel.GetComponent<Spell_Tooltip> ();
		UpdateInteractable ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void MouseEnter () {
//		toolTipPanel.SetActive (true);
		s_tooltip.SetInfo (spell.s_name, spell.s_desc, spell.thisSpellType.ToString ());
	}

	public void MouseExit () {
//		toolTipPanel.SetActive (false);
		s_tooltip.ClearInfo ();
	}

	public void MouseClick () {
		if (iconInteractable) {
			spell.LearnSpell ();
			UpdateInteractable ();
		}
	}

	public void SetInteractable (bool state) {
		iconInteractable = state;
		if (state) {
			thisImage.color = new Color (1, 1, 1, 1);
		} else {
			thisImage.color = new Color (1, 1, 1, 0.5f);
		}
	}

	public void UpdateInteractable () {
		SetInteractable (spell.unlocked);
		SetLearned (spell.learned);
	}

	void OnEnable () {
		UpdateInteractable ();
	}

	void SetLearned (bool state) {
		learnedImage.enabled = state;
	}


}
