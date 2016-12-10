using UnityEngine;
using System.Collections;

public class ConsoleCommand : MonoBehaviour {
	public static ConsoleCommand consoleCommand;
	// Use this for initialization
	void Start () {
		consoleCommand = GetComponent<ConsoleCommand> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.BackQuote)) {
			
		}
	}

	void ToggleCC () {
		
	}
}
