using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Manager : MonoBehaviour {

	public static UI_Manager ui;
	Player_Script player;
	Slider healthSlider, manaSlider;

	void Awake () {
		ui = GetComponent<UI_Manager> ();
	}

	void Start () {
		player = Player_Script.player;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SliderUpdate () {
		healthSlider.value = player.Health;
		manaSlider.value = player.Mana;
	}
}
