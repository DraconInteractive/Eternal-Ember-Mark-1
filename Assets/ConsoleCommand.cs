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
			break;
		case "LoadGame":
			player.LoadGame ();
		}
		ToggleCC (false);
	}
}
