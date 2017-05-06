using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class ConsoleCommand : MonoBehaviour {
	public InputField commandConsole;
	private Player player;
	Player_UI p_UI;

	public bool canCommand;
	// Use this for initialization
	void Start () {
		player = Player.player;
		p_UI = Player_UI.p_UI;
		canCommand = false;
		ToggleCC (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.BackQuote)) {
			ToggleCC (!commandConsole.gameObject.activeSelf);
		}
	}

	void ToggleCC (bool state) {
		commandConsole.gameObject.SetActive (state);
		player.ToggleCursor(state);

		Player_Movement.p_movement.canMove = !state;
	}

	public void EnterCommand (string command) {
		if (!canCommand) {
			switch (command)
			{
			case "Logon: Dracon_Master":
				canCommand = true;
				p_UI.SpawnEventText ("Logon Accepted. Welcome.");
				break;
			case "LDM":
				canCommand = true;
				p_UI.SpawnEventText ("You lazy bastard");
				break;
			default:
				p_UI.SpawnEventText ("Please log in to submit commands");
				break;
			}

		} else {
			switch (command) {
			case "?":
				p_UI.SpawnEventText ("Test | ProgressionMarker_DF | Load_MainScene | SetSpell | Logout");
				break;
			case "Test":
				p_UI.SpawnEventText ("Command Accepted");
				break;
			case "ProgressionMarker_DF":
				Player_Progression.p_prog.UpdateProgression("dragonFound", true);
				break;
			case "Check_PM_DF":
				bool check;
				print ("Dragon_Found_PM: " + Player_Progression.p_prog.progression.TryGetValue("dragonFound", out check));
				break;
			case "Load_MainScene":
//				LoadingScreen.ls.StartCoroutine(LoadLevel ("Main Scene"));
				LoadingScreen.ls.StartCoroutine (LoadingScreen.ls.LoadLevel ("Main Scene"));
				break;
			case "SetSpellOne":
				p_UI.SpawnEventText ("Setting Spell_Socket_One to spell four");
				Player_SpellControl.spellControl.SetSpell (1, 4);
				break;
			case "SetSpellTwo":
				p_UI.SpawnEventText ("Setting Spell_Socket_Two to spell one");
				Player_SpellControl.spellControl.SetSpell (2, 1);
				break;
			case "SetSpellThree":
				p_UI.SpawnEventText ("Setting Spell_Socket_Two to spell one");
				Player_SpellControl.spellControl.SetSpell (3, 5);
				break;
			case "SetSpellFour":
				p_UI.SpawnEventText ("Setting Spell_Socket_Two to spell one");
				Player_SpellControl.spellControl.SetSpell (4, 1);
				break;
			case "Logout":
				canCommand = false;
				p_UI.SpawnEventText ("Logged Out");
				break;
			
			}
		}

		commandConsole.text = "";
		ToggleCC (false);
	}
}
