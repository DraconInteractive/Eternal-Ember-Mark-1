using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Interactable {

	Player player;
	Player_Inventory p_inv;

	public Vector3 handPosOffset, handRotOffset;
	GameObject hand, holster;

	public EquipmentItem thisItem;
	// Use this for initialization
	void Start () {
		player = Player.player;
		p_inv = Player_Inventory.p_inventory;
	}

	public override void Interact ()
	{
		p_inv.AddItemToInventory (thisItem, 1);
		Destroy (this.gameObject);
	}

	public void Equip () {
		if (player == null) {
			player = Player.player;
		}
		hand = player.leftHandAttach;
		holster = player.GetComponent<Player_Holsters>().oh_shield;
		Holster ();
		player.playerShield = GetComponent <Shield> ();
	}

	public void UnEquip () {
		player.playerShield = null;
		Destroy (this.gameObject);
	}

	public void Holster () {
		transform.SetParent (holster.transform);
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.Euler (Vector3.zero);
	}

	public void UnHolster () {
		transform.SetParent (hand.transform);
		transform.localPosition = handPosOffset;
		transform.localRotation = Quaternion.Euler (handRotOffset);
	}
}
