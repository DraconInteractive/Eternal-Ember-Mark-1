using UnityEngine;
using System.Collections;

public class Player_Script : MonoBehaviour {
	public static Player_Script player;

	public AdvancedSaveSystem_SaveGO goSaver;
	public AdvancedSaveSystem_LoadGO goLoader;

	void Awake () {
		player = GetComponent<Player_Script> ();
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SaveGame () {
		goSaver.SaveGameObjects (1);
	}

	public void LoadGame () {
		goLoader.LoadGameObjects (1);
	}
}
