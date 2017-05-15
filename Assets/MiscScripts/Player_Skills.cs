using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Skills : MonoBehaviour {

	public static Player_Skills p_skills;
	Player player;
	Player_Movement p_movement;
	PossessionController pc_controller;
	Player_Progression p_prog;
	public GameObject skillPanel;
	public bool skillsOpen;

	public Button combatTab, fireTab, masteryTab;
	[HideInInspector]
	public GameObject openTab;

	public GameObject combatIcons, fireIcons, masteryIcons;

	public Spell_Icon[] allIcons;
	Color cC, fC, mC;
	void Awake () {
		p_skills = GetComponent<Player_Skills> ();
		combatTab.onClick.AddListener (() => SetOpenTab (combatTab.gameObject));
		fireTab.onClick.AddListener (() => SetOpenTab (fireTab.gameObject));
		masteryTab.onClick.AddListener (() => SetOpenTab (masteryTab.gameObject));

		cC = combatTab.image.color;
		fC = fireTab.image.color;
		mC = masteryTab.image.color;
	}

	void Start () {
		player = Player.player;
		p_movement = Player_Movement.p_movement;
		pc_controller = PossessionController.pc_controller;
		p_prog = Player_Progression.p_prog;
		ToggleSkills (false);
		SetOpenTab (null);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.K)) {
			if (p_movement.canMove) {
				ToggleSkills (!skillsOpen);
			} else {
				ToggleSkills (false);
			}

		}
	}

	public void ToggleSkills (bool state) {
		if (pc_controller.currentHost != PossessionController.hostType.PLAYER && state == true) {
			return;
		}

		skillPanel.SetActive (state);
		player.ToggleCursor (state);
		skillsOpen = state;

		if (state) {
			player.ToggleCursor (true);
			p_movement.canMove = false;
			SetOpenTab (null);
			bool foundDragon = p_prog.progression ["foundDragon"];
			bool communedWithDragon;
//			p_prog.progression.TryGetValue ("foundDragon", out foundDragon);
			fireTab.interactable = foundDragon;
			print ("fd: " + foundDragon);
			p_prog.progression.TryGetValue ("communedWithDragon", out communedWithDragon);
			masteryTab.interactable = communedWithDragon;
		} else {
			player.ToggleCursor (false);
			p_movement.canMove = false;
		}
	}

	public void SetOpenTab (GameObject tabToSet) {
		if (tabToSet == openTab || tabToSet == null) {
			combatIcons.SetActive (false);
			fireIcons.SetActive (false);
			masteryIcons.SetActive (false);

			combatTab.image.color = cC;
			fireTab.image.color = fC;
			masteryTab.image.color = mC;

			openTab = null;
			return;
		}

		if (tabToSet == combatTab.gameObject) {
			combatIcons.SetActive (true);
			fireIcons.SetActive (false);
			masteryIcons.SetActive (false);

			combatTab.image.color = cC;
			fireTab.image.color = new Color (1, 1, 1, 0.5f);
			masteryTab.image.color = new Color (1, 1, 1, 0.5f);
		} else if (tabToSet == fireTab.gameObject) {
			combatIcons.SetActive (false);
			fireIcons.SetActive (true);
			masteryIcons.SetActive (false);

			combatTab.image.color = new Color (1, 1, 1, 0.5f);
			fireTab.image.color = fC;
			masteryTab.image.color = new Color (1, 1, 1, 0.5f);
		} else if (tabToSet == masteryTab.gameObject) {
			combatIcons.SetActive (false);
			fireIcons.SetActive (false);
			masteryIcons.SetActive (true);

			combatTab.image.color = new Color (1, 1, 1, 0.5f);
			fireTab.image.color = new Color (1, 1, 1, 0.5f);
			masteryTab.image.color = mC;
		}
		openTab = tabToSet;
	}
}
