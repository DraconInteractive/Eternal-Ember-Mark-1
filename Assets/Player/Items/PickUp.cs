using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : Interactable {

	public Item pickupItem;
	public int itemQuantity;

	public override void Interact(){
		Player_Inventory.p_inventory.AddItemToInventory (pickupItem, itemQuantity);
		Destroy (this.gameObject);
	}

}
