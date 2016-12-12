using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConsoleCommand : MonoBehaviour {
	public InputField commandConsole;
	private Player_Script player;
	// Use this for initialization
	void Start () {
		ToggleCC (false);
		player = Player_Script.player;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.BackQuote)) {
			ToggleCC (!commandConsole.gameObject.activeSelf);
		}
	}

	void ToggleCC (bool state) {
		commandConsole.gameObject.SetActive (state);
	}

	public void EnterCommand (string command) {
		switch (command)
		{
		case "Test":
			print ("Command Accepted");
			break;
		case "SaveGame":
			player.SaveGame ();
			print ("Player Saved");
			break;
		case "LoadGame":
			player.LoadGame ();
			print ("Player Loaded");
			break;
		case "SetGender_Male":
			player.SetGender (Player_Script.genderType.Male);
			break;
		case "SetGender_Female":
			player.SetGender (Player_Script.genderType.Female);
			break;
		case "SetClass_Rogue":
			player.SetClass (Player_Script.classType.Rogue);
			break;
		case "SetClass_Warrior":
			player.SetClass (Player_Script.classType.Warrior);
			break;
		case "Damage_Player_20":
			player.DamagePlayer (20);
			break;
		case "Heal_Player_20":
			player.HealPlayer (20);
			break;
		}
		ToggleCC (false);
	}
}
