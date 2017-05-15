using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCheck : MonoBehaviour {
	public static PlayerCheck p_check;
	public GameObject playerPrefab, playerSpawn;
	// Use this for initialization

//	public delegate 
	void Awake () {
		p_check = GetComponent<PlayerCheck>();
	}
		
	void OnEnable () {
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnDisable () {
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}

	void OnLevelFinishedLoading (Scene scene, LoadSceneMode mode) {
		if (Player.player == null) {
			GameObject p = Instantiate (playerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			Player pl = p.GetComponentInChildren<Player> ();
			pl.transform.position = playerSpawn.transform.position;
		} else {
			Player.player.transform.position = playerSpawn.transform.position;
		}
	}
}
