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
			default:
				p_UI.SpawnEventText ("Please log in to submit commands");
				break;
			}

		} else {
			switch (command) {
			case "Test":
				p_UI.SpawnEventText ("Command Accepted");
				break;
			case "ProgressionMarker_DF":
				Player_Progression.p_prog.OnDragonFound ();
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
