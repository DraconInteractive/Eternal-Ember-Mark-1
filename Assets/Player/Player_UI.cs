using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_UI : MonoBehaviour {
	public static Player_UI p_UI;

	Player player;
	public GameObject eventTextPanel, eventTextPrefab;

	public Slider healthSlider;
	public Text healthText;

	public enum UI_Element {Quest, Skill, Rune, None};
	public bool questP, skillP, runeP;
	public GameObject questPanel, skillPanel, runePanel;
//	public 
	// Use this for initialization
	void Awake () {
		p_UI = GetComponent<Player_UI> ();
	}

	void Start () {
		player = Player.player;
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

	public void SetElement (UI_Element ui_e) {
		if (player == null) {
			player = Player.player;
		}

		switch (ui_e)
		{
		case UI_Element.Quest:
			questP = !questP;
			questPanel.SetActive (questP);
			break;
		case UI_Element.Rune:
			runeP = !runeP;
			runePanel.SetActive (runeP);
			break;
		case UI_Element.Skill:
			skillP = !skillP;
			skillPanel.SetActive (skillP);
			break;
		}

		if (questP == true || runeP == true || skillP == true) {
			player.ToggleCursor (true);
			Player_Movement.p_movement.canMove = false;
		} else {
			player.ToggleCursor (false);
			Player_Movement.p_movement.canMove = true;
		}
	}
	public void SetElement (UI_Element ui_e, bool state) {
		if (player == null) {
			player = Player.player;
		}

		switch (ui_e)
		{
		case UI_Element.Quest:
			questP = state;
			questPanel.SetActive (state);
			break;
		case UI_Element.Rune:
			runeP = state;
			runePanel.SetActive (state);
			break;
		case UI_Element.Skill:
			skillP = state;
			skillPanel.SetActive (state);
			break;
		}

		if (questP == true || runeP == true || skillP == true) {
			player.ToggleCursor (true);
		} else {
			player.ToggleCursor (false);
		}
	}
}
