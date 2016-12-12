using UnityEngine;
using System.Collections;

public class Player_Script : MonoBehaviour {
	public static Player_Script player;

	public enum genderType {Male, Female};
	public genderType playerGender;
	public GameObject maleModel, femaleModel;
	void Awake () {
		player = GetComponent<Player_Script> ();
	}
	// Use this for initialization
	void Start () {
		SetGender (genderType.Male);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SaveGame () {
		PlayerPrefs.SetString ("PlayerGender", playerGender.ToString ());
	}

	public void LoadGame () {
		string g = PlayerPrefs.GetString ("PlayerGender");
		print (g);
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
	}

	public void SetGender (genderType gender) {
		switch (gender)
		{
		case genderType.Male:
			maleModel.SetActive (true);
			femaleModel.SetActive (false);
			break;
		case genderType.Female:
			femaleModel.SetActive (true);
			maleModel.SetActive (false);
			break;
		}

		playerGender = gender;
	}
}
