using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_UI : MonoBehaviour {
	public static Player_UI p_UI;

	public GameObject eventTextPanel, eventTextPrefab;

	public Slider healthSlider;
	public Text healthText;
	// Use this for initialization
	void Awake () {
		p_UI = GetComponent<Player_UI> ();
	}
	
	// Update is called once per frame
//	void Update () {
//		
//	}

	public void SpawnEventText (string etext) {
		GameObject eventText = Instantiate (eventTextPrefab, eventTextPanel.transform) as GameObject;
		eventText.transform.localScale = Vector3.one;
		eventText.GetComponent<Text> ().text = etext;
	}

	public void UpdateHealthSlider (float health, float maxHealth) {
		healthSlider.value = health;
		healthText.text = Mathf.Round (health).ToString() + "/" + maxHealth.ToString();
	}
}
