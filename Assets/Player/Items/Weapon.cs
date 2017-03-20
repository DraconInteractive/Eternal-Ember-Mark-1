using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Interactable{
	Player player;
	Player_Inventory p_inv;
	public Vector3 handPosOffset, handRotOffset;

	GameObject hand, holster;

	public bool equipped;

	public bool attacking;

	public EquipmentItem thisItem;

	public enum weaponType {MH_SWORD, MH_DAGGER, OH_DAGGER};
	public weaponType w_type;
	void Start () {
		player = Player.player;
		p_inv = Player_Inventory.p_inventory;
	}		

	public override void Interact () {
		p_inv.AddItemToInventory (thisItem, 1);
		Destroy (this.gameObject);
	}

	public void Equip () {
		if (player == null) {
			player = Player.player;
		}
		hand = player.rightHandAttach;
//		holster = player.gameObject.GetComponent<Player_Holster>().singleSwordHolster;
		switch (w_type)
		{
		case weaponType.MH_SWORD:
			holster = player.GetComponent<Player_Holsters> ().mh_sword;
			break;
		case weaponType.MH_DAGGER:
			holster = player.GetComponent<Player_Holsters> ().mh_dagger;
			break;
		case weaponType.OH_DAGGER:
			holster = player.GetComponent<Player_Holsters> ().oh_dagger;
			break;
		}

		Holster ();
		player.playerWeapon = GetComponent<Weapon> ();
	}

	public void UnEquip () {
		player.playerWeapon = null;
		Destroy (this.gameObject);

//		Item i = slotItem;
//		EmptyItemSlot ();
//		Player_Inventory.p_inventory.AddItemToInventory (i, 1);
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
