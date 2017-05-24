using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Container : MonoBehaviour {

	public static Player_Container p_container;
	Player player;

	void Awake () {
		p_container = GetComponent<Player_Container> ();
	}

	// Use this for initialization
	void Start () {
		player = Player.player;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
