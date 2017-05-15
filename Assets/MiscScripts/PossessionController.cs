using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PossessionController : MonoBehaviour {
	public static PossessionController pc_controller;
	Player player;
	[HideInInspector]
	public Dragon dragon;

	public enum hostType {DRAGON, PLAYER, NPC};
	public hostType currentHost;

	public GameObject playerCamera, dragonCamera;

	public bool dragonInScene;

	bool isInit;
	void Awake () {
		pc_controller = GetComponent<PossessionController> ();
	}
	// Use this for initialization
	void Start () {
		InitiatePossession ();
	}

//	void OnEnable () {
//		SceneManager.sceneLoaded += OnSceneFinaliseChange;
//	}
//
//	void OnDisable () {
//		SceneManager.sceneLoaded -= OnSceneFinaliseChange;
//	}

//	public void OnSceneFinaliseChange (Scene Scene, LoadSceneMode mode) {
//		InitiatePossession ();
//		print ("Reinitialising possession");
//	}
		
	public void InitiatePossession () {
		player = Player.player;
		playerCamera = player.playerCamera;
		SearchDragonPresent ();
		SetHost (hostType.PLAYER, isInit);
		isInit = false;
	}
	public void SearchDragonPresent () {
		dragon = Dragon.dragon;

		if (dragon != null){
			print ("Found Dragon");
			dragonInScene = true;
		} else {
			print ("Dragon Not Found");
			dragonInScene = false;
			return;
		}

		dragonCamera = dragon.dragonCamera;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.V)) {
			if (dragonInScene) {
				SwitchHost ();
			} else {
				SearchDragonPresent ();

				if (dragonInScene) {
					SwitchHost ();
				} else {
					print ("No Dragon");
				}

			}

		}
	}

	public void SetHost (hostType host, bool isInit) {
		print ("Setting Host" + host.ToString ());
		if (currentHost == host) {
			print ("Host = current host");
			return;
		}

		switch (host)
		{
		case hostType.DRAGON:
			print ("d1");
			playerCamera.SetActive (false);
			dragonCamera.SetActive (true);
			player.gameObject.GetComponent<Player_Movement> ().enabled = false;
			dragon.gameObject.GetComponent<Dragon_Movement> ().enabled = true;
			player.anim.SetTrigger ("StartPossess");
			player.possessed = false;
			break;
		case hostType.PLAYER:
			playerCamera.SetActive (true);
			playerCamera.GetComponent<Player_Camera> ().inControl = true;
			player.gameObject.GetComponent<Player_Movement> ().enabled = true;

			if (dragonInScene) {
				dragonCamera.SetActive (false);
				dragon.gameObject.GetComponent<Dragon_Movement> ().enabled = false;
			}

			if (!isInit) {
				player.anim.SetTrigger ("EndPossess");
			}
			player.possessed = true;
			break;
		case hostType.NPC:
			playerCamera.GetComponent<Player_Camera> ().inControl = false;
			player.gameObject.GetComponent<Player_Movement> ().enabled = false;

			if (dragonInScene) {
				dragonCamera.SetActive (false);
				dragon.GetComponent<Dragon_Movement> ().enabled = false;
			}

			player.possessed = false;
			break;
		}

		currentHost = host;
	}

	void SwitchHost () {
		if (!dragonInScene) {
			return;
		}
		print ("Switch");
		if (currentHost == hostType.PLAYER) {
			SetHost (hostType.DRAGON, false);
		} else {
			SetHost (hostType.PLAYER, false);
		}
	}
}
