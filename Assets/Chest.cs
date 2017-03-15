using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable {

	public GameObject chestLid;

	public bool isOpen;

	public List<Item> c_items;

	public override void Interact () {
		if (isOpen) {
			Close ();
		} else {
			Open ();
		}
	}
	// Update is called once per frame
	public void Open () {
		if (isOpen) {
			Close ();
			return;
		} else {
			isOpen = true;
		}
		chestLid.transform.Rotate (new Vector3 (-90, 0, 0));
		for (int i = 0; i < c_items.Count; i++) {
			Player_Inventory.p_inventory.AddItemToInventory (c_items [i], 1);
			c_items.Remove (c_items [i]);
		}
	}

	public void Close () {
		if (!isOpen) {
			Open ();
		} else {
			isOpen = false;
		}
		chestLid.transform.Rotate (new Vector3 (90, 0, 0));
		isOpen = false;
	}
}
