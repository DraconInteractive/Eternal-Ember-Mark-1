using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_SpellControl : MonoBehaviour {

	public static Player_SpellControl spellControl;

	public Transform CharacterAttachPoint;
	public Transform CharacterAttachPoint_L;
	public Transform AttachPoint;

	[Header("Spell_One")]
	public GameObject sOne_CharacterEffect;
	public GameObject sOne_CharacterEffect_L;
	public GameObject sOne_Effect;

	[Header("Spell_Two")]

	public GameObject sTwo_CharacterEffect;
	public GameObject sTwo_CharacterEffect_L;
	public GameObject sTwo_Effect;

	[Header("Spell_Three")]

	public GameObject sThree_Effect;

	[Header("Spell_Four")]

	public GameObject sFour_Effect;

	[Header("Spell_Five")]

	public GameObject sFive_Effect;
	public int sFive_k_prompt;

	[HideInInspector]
	public int spellOne, spellTwo, spellThree, spellFour;

	void Awake () {
		spellControl = GetComponent<Player_SpellControl> ();
	}

	void Start () {
		SetSpell (1, 1);
		SetSpell (2, 2);
		SetSpell (3, 3);
		SetSpell (4, 4);
	}
	void OnEnable()
	{
		DeactivateEffect ();
	}

	public void DeactivateEffect () {
		//----------------------------------
		if (sOne_Effect!=null) {
			sOne_Effect.SetActive(false);
		}

		if (sOne_CharacterEffect != null)
		{
			sOne_CharacterEffect.SetActive(false);
		}

		if (sOne_CharacterEffect_L != null)
		{
			sOne_CharacterEffect_L.SetActive(false);
		}
		//----------------------------------
		if (sTwo_Effect != null) {
			sTwo_Effect.SetActive (false);
		}

		if (sTwo_CharacterEffect != null) {
			sTwo_CharacterEffect.SetActive (false);
		}

		if (sTwo_CharacterEffect_L != null) {
			sTwo_CharacterEffect_L.SetActive (false);
		}
		//----------------------------------
		if (sThree_Effect != null) {
			sThree_Effect.SetActive (false);
		}
		//----------------------------------
		if (sFour_Effect != null) {
			sFour_Effect.SetActive (false);
		}
		//----------------------------------
		if (sFive_Effect != null) {
			sFive_Effect.SetActive (false);
		}
	}

	public void DeactivateEffect (int effectNum) {
		switch (effectNum)
		{
		case 1:
			if (sOne_Effect != null) {
				sOne_Effect.SetActive (false);
			}

			if (sOne_CharacterEffect != null) {
				sOne_CharacterEffect.SetActive (false);
			}

			if (sOne_CharacterEffect_L != null)
			{
				sOne_CharacterEffect_L.SetActive(false);
			}
			break;
		case 2:
			if (sTwo_Effect != null) {
				sTwo_Effect.SetActive (false);
			}

			if (sTwo_CharacterEffect != null) {
				sTwo_CharacterEffect.SetActive (false);
			}

			if (sTwo_CharacterEffect_L != null) {
				sTwo_CharacterEffect_L.SetActive (false);
			}
			break;
		case 3:
			if (sThree_Effect != null) {
				sThree_Effect.SetActive (false);
			}
			break;
		case 4:
			if (sFour_Effect != null) {
				sFour_Effect.SetActive (false);
			}
			break;
		case 5:
//			if (sFive_Effect != null) {
//				sFive_Effect.SetActive (false);
//			}

//			GameObject.Find ("SpellFive").GetComponent<Spell_Attached>().;
			break;
		}

	}
		
	public void ActivateEffect(int effectNum)
	{
		switch (effectNum) {
		case 1:
			if (sOne_Effect == null)
				break;
//			sOne_Effect.SetActive (true);
			GameObject spellOne = Instantiate (sOne_Effect, sOne_Effect.transform.position, sOne_Effect.transform.rotation) as GameObject;
			spellOne.GetComponent<Spell_Attached> ().attached = false;
			spellOne.SetActive (true);
			break;
		case 2:
			if (sTwo_Effect == null)
				break;
//			sTwo_Effect.SetActive (true);
			GameObject spellTwo = Instantiate (sTwo_Effect, sTwo_Effect.transform.position, sTwo_Effect.transform.rotation) as GameObject;
			spellTwo.GetComponent<Spell_Attached> ().attached = false;
			spellTwo.SetActive (true);
			break;
		case 5:
			if (sFive_Effect == null) {
				print ("sFive Missing");
				break;
			} 
//			sFive_Effect.SetActive(true);
			GameObject spellFive = Instantiate (sFive_Effect, sFive_Effect.transform.position, sFive_Effect.transform.rotation) as GameObject;
			spellFive.GetComponent<Spell_Attached> ().attached = false;
			spellFive.GetComponent<Spell_Attached> ().kFivePrompt = sFive_k_prompt;
			spellFive.SetActive (true);
			break;
		}
		
	}

	public void ActivateCharacterEffect(int effectNum)
	{
		switch (effectNum)
		{
		case 1:
			if (sOne_CharacterEffect == null) break;
			sOne_CharacterEffect.SetActive(true);
			if (sOne_CharacterEffect_L == null) break;
			sOne_CharacterEffect_L.SetActive(true);
			break;
		case 2:
			if (sTwo_CharacterEffect == null) return;
			sTwo_CharacterEffect.SetActive(true);

//			if (sTwo_CharacterEffect_L == null) return;
//			sTwo_CharacterEffect_L.SetActive(true);
			break;
		case 3:
			if (sThree_Effect == null) {
				print ("sThree Missing");
				break;
			} 
//			sThree_Effect.SetActive(true);
			GameObject spellThree = Instantiate (sThree_Effect, sThree_Effect.transform.position, sThree_Effect.transform.rotation) as GameObject;
			spellThree.GetComponent<Spell_Attached> ().attached = false;
			spellThree.SetActive (true);
			break;
		case 4:
			if (sFour_Effect == null) {
				print ("sFour Missing");
				break;
			} 
//			sFour_Effect.SetActive(true);
			GameObject spellFour = Instantiate (sFour_Effect, sFour_Effect.transform.position, sFour_Effect.transform.rotation) as GameObject;
			spellFour.GetComponent<Spell_Attached> ().attached = false;
			spellFour.SetActive (true);
			break;
		
		}
	}

	public void ActivateCharacterEffect2 (int effectNum) {
		switch (effectNum)
		{
		case 2:
			if (sTwo_CharacterEffect_L == null) return;
			sTwo_CharacterEffect_L.SetActive(true);
			break;
		}
	}
		
	void LateUpdate()
	{
		if (sOne_Effect != null && AttachPoint != null)
		{
			sOne_Effect.transform.position = AttachPoint.position;
		}
		if (sOne_CharacterEffect != null && CharacterAttachPoint != null)
		{
			sOne_CharacterEffect.transform.position = CharacterAttachPoint.position;
		}

		if (sOne_CharacterEffect_L != null && CharacterAttachPoint_L != null)
		{
			sOne_CharacterEffect_L.transform.position = CharacterAttachPoint_L.position;
		}

		if (sTwo_Effect != null && AttachPoint != null) {
			sOne_Effect.transform.position = AttachPoint.position;
		}
		if (sOne_CharacterEffect != null && CharacterAttachPoint != null) {
			sOne_CharacterEffect.transform.position = CharacterAttachPoint.position;
		}

		if (sThree_Effect != null && AttachPoint != null) {
			sThree_Effect.transform.position = AttachPoint.position;
		}

		if (sFour_Effect != null && AttachPoint != null) {
			sFour_Effect.transform.position = AttachPoint.position;
		}

//		if (sFive_Effect != null && AttachPoint != null) {
//			sFive_Effect.transform.position = AttachPoint.position;
//		}
	}

	public void SetSpell (int socket, int target) {
		target = Mathf.Clamp (target, 1, 5);

		if (spellOne == target || spellTwo == target || spellThree == target || spellFour == target) {
			print ("spell already assigned");
			return;
		} else {
			switch (socket)
			{
			case 1:
				spellOne = target;
				break;
			case 2:
				spellTwo = target;
				break;
			case 3:
				spellThree = target;
				break;
			case 4:
				spellFour = target;
				break;
			}
		}
	}
}
