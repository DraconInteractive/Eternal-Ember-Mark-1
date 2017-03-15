using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Interactable{
	Player player;
	public Vector3 handPosOffset, handRotOffset;

	GameObject hand, holster;

	public bool equipped;

	public bool attacking;
	void Start () {
		player = Player.player;
	}

	public override void Interact () {
		hand = player.rightHandAttach;
		holster = player.backHolster;
		Holster ();
		player.playerWeapon = GetComponent<Weapon> ();
	}

	/// <summary>
	/// Place weapon in holster
	/// </summary>
	public void Holster () {
		transform.SetParent (holster.transform);
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.Euler(Vector3.zero);
	}

	/// <summary>
	/// Place weapon in hand
	/// </summary>
	public void UnHolster () {
		transform.SetParent (hand.transform);
		transform.localPosition = handPosOffset;
		transform.localRotation = Quaternion.Euler(handRotOffset);
	}
		
}
