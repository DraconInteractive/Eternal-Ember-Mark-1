using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using MORPH3D;

public class Player_Script : MonoBehaviour {
	public static Player_Script player;
	private M3DCharacterManager maleM3DManager, femaleM3DManager;
	private Rigidbody rb;

	//Stats
	public enum genderType {Male, Female};
	public genderType playerGender;
	public GameObject maleModel, femaleModel;

	public enum classType {Warrior, Rogue};
	public classType playerClass;

	public List<MORPH3D.COSTUMING.CIclothing> warriorClothing;
	public Slider healthSlider, manaSlider;
	public float currentHealth, maxHealth, currentMana, maxMana;

	//Movement
	public float playerRunSpeed, playerTurnSpeed;
	public Animator maleAnim, femaleAnim;
	private Animator playerAnim;
	void Awake () {
		player = GetComponent<Player_Script> ();
		rb = GetComponent<Rigidbody> ();
		maleM3DManager = maleModel.GetComponent<M3DCharacterManager> ();
		femaleM3DManager = femaleModel.GetComponent<M3DCharacterManager> ();
	}
	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;
		currentMana = maxMana;
		SetGender (genderType.Male);
		SetClass (classType.Warrior);

	}
	
	// Update is called once per frame
	void Update () {
		PlayerMovement ();
		UpdateAnimation ();
	}

	#region Movement
	private void PlayerMovement () {
		float prs;
		if (Input.GetAxis("Vertical") >= 0) {
			prs = playerRunSpeed;
		} else {
			prs = playerRunSpeed / 2;
		}
		rb.MovePosition (transform.position + transform.forward * Input.GetAxis ("Vertical") * prs * Time.deltaTime);

		transform.Rotate (transform.up, Input.GetAxis("Horizontal") * playerTurnSpeed * Time.deltaTime);
	}

	private void UpdateAnimation () {
		if (playerAnim == null) {
			if (playerGender == genderType.Male) {
				playerAnim = maleAnim;
			} else if (playerGender == genderType.Female) {
				playerAnim = femaleAnim;
			} else {
				Debug.Log ("No Animation Gender Assignable");
			}
		}

		playerAnim.SetFloat ("Speed", Input.GetAxis ("Vertical"));
	}
	#endregion

	#region data retainment
	public void SaveGame () {
		PlayerPrefs.SetString ("PlayerGender", playerGender.ToString ());
		PlayerPrefs.SetString ("PlayerClass", playerClass.ToString ());
	}

	public void LoadGame () {
		string g = PlayerPrefs.GetString ("PlayerGender");
//		print (g);
		if (g == "Male") {
			if (playerGender.ToString () != g) {
				SetGender (genderType.Male);
			}
		} else if (g == "Female") {
			if (playerGender.ToString () != g) {
				SetGender (genderType.Female);
			}
		} else {
			print ("Unknown Gender");
		}

		string c = PlayerPrefs.GetString ("PlayerClass");
//		print (c);
		if (c == "Rogue") {
			playerClass = classType.Rogue;
		} else if (c == "Warrior") {
			playerClass = classType.Warrior;
		} else {
			print ("Unknown Class");
		}
	}

	#endregion

	#region player statstics

	public void DamagePlayer (int amount) {
		currentHealth -= amount;
		healthSlider.value = currentHealth;
	}

	public void HealPlayer (int amount) {
		currentHealth += amount;
		healthSlider.value = currentHealth;
	}

	public void SetGender (genderType gender) {
		switch (gender)
		{
		case genderType.Male:
			maleModel.SetActive (true);
			femaleModel.SetActive (false);
			playerAnim = maleAnim;
			break;
		case genderType.Female:
			femaleModel.SetActive (true);
			maleModel.SetActive (false);
			playerAnim = femaleAnim;
			break;
		}

		playerGender = gender;
	}

	public void SetClass (classType c) {
		switch (c) 
		{
		case classType.Rogue:
			break;
		case classType.Warrior:
			break;
		}
	}

	#endregion
}
