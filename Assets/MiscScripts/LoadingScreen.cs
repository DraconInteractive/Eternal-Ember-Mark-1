using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour {

	public static LoadingScreen ls;

	public Slider levelProgress;

	AsyncOperation loadOp;

	public GameObject[] comps;

	void Awake () {
		ls = GetComponent<LoadingScreen> ();
	}

	void Start () {
		SetComponentState (false);
		levelProgress.minValue = 0;
		levelProgress.maxValue = 1;
		levelProgress.value = 0;
	}

	void Update () {
//		if (Input.GetKeyDown(KeyCode.T)) {
//			StartCoroutine (LoadLevel ("Main Scene"));
//			print ("Loading Main Scene");
//		}
	}
		
	public IEnumerator LoadLevel (string levelName) {
		print ("Loading Level: " + levelName);
		SetComponentState (true);

		loadOp = SceneManager.LoadSceneAsync (levelName);

		while (loadOp.isDone == false) {
			levelProgress.value = loadOp.progress;
			print (loadOp.progress);
			yield return null;
		}

		transform.position = PlayerCheck.p_check.playerSpawn.transform.position;
		transform.rotation = PlayerCheck.p_check.playerSpawn.transform.rotation;
		SetComponentState (false);
		yield break;
	}

	public void SetComponentState (bool state) {
		foreach (GameObject g in comps) {
			g.SetActive (state);
		}
	}
		
//	// Use this for initialization
//	void Start () {
//		ToggleLoadingScreen (loadingTime);
//	}
//
//	void ToggleLoadingScreen (bool state) {
//		gameObject.SetActive (state);
//	}
//
//	void ToggleLoadingScreen (float timer) {
//		ToggleOn ();
//		Invoke ("ToggleOff", timer);
//	}
//
//	void ToggleOn () {
//		gameObject.SetActive (true);
//	}
//
//	void ToggleOff () {
//		gameObject.SetActive (false);
//	}
}
