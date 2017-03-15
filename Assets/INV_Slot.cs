using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class INV_Slot : MonoBehaviour {

	public Item slotItem;

	public bool hasItem;

	public string itemName;
	public int itemQuantity;
	public Sprite itemSprite;

	public Image slotImage;
	public Sprite defaultSprite;

	public void RecieveItem (Item item, int quantity) {
		hasItem = true;

		itemName = item.itemName;
		itemQuantity = quantity;

		if (item.itemSprite != null) {
			itemSprite = item.itemSprite;
			slotImage.sprite = itemSprite;
		}

		slotItem = item;
	}

	public void EmptyItemSlot () {
		hasItem = false;
		itemName = "";
		itemQuantity = 0;
		itemSprite = null;

		slotImage.sprite = defaultSprite;
	}
}
