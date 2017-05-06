using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class INV_Slot : MonoBehaviour {
	[HideInInspector]
	public Player_Inventory p_inv;
	public Item slotItem;

	public bool hasItem;

	public string itemName;
	public int itemQuantity;
	public Sprite itemSprite;

	public Image slotImage, pulseIMG;
	public Sprite defaultSprite;
	[HideInInspector]
	public bool newItem;

	public GameObject slotDraggable, itemOptionPanel, pickupTemplate;
	void Start () {
		SlotStart ();
	}

	public virtual void SlotStart () {
//		SetNewItem (false);
		p_inv = Player_Inventory.p_inventory;
	}

	public virtual void RecieveItem (Item item, int quantity) {
		hasItem = true;

		itemName = item.itemName;
		itemQuantity = quantity;

		if (item.itemSprite != null) {
			itemSprite = item.itemSprite;
			slotImage.sprite = itemSprite;
		}

		slotItem = item;

		item.slot = this.gameObject;

		SetNewItem (true);
	}

	public virtual void EmptyItemSlot () {
		hasItem = false;
		itemName = "";
		itemQuantity = 0;
		itemSprite = null;

		slotImage.sprite = defaultSprite;

		slotItem.slot = null;
		slotItem = null;

		SetNewItem (false);
	}

	public void SetNewItem (bool state) {
		newItem = state;
		if (state) {
			pulseIMG.color = new Color (1, 1, 1, 1);
		} else {
			pulseIMG.color = new Color (1, 1, 1, 0);
		}
	}

	public virtual void RightClick () {
		//UseSlot();
		SpawnItemOptionPanel ();
	}

	public virtual void UseSlot () {
		if (slotItem is EquipmentItem) {
			p_inv.EquipItem (slotItem as EquipmentItem);
		}
		if (slotItem is ConsumableItem) {
			(slotItem as ConsumableItem).ConsumableEffect ();
		}
	}

	public virtual void DropItem () {
		if (slotItem != null) {
			Player player = Player.player;

			GameObject g = Instantiate (pickupTemplate, player.transform.position + Vector3.up * 0.25f, Quaternion.identity) as GameObject;
			g.GetComponent<PickUp> ().pickupItem = slotItem;
			EmptyItemSlot ();
		}
	}

	public virtual void DestroyItem () {
		EmptyItemSlot ();
	}
		
	public virtual void LeftClick () {
		if (slotItem != null) {
			GameObject go = Instantiate (slotDraggable, Input.mousePosition, Quaternion.identity, Player_Inventory.p_inventory.inventoryPanel.transform) as GameObject;
			go.transform.localScale = Vector3.one;
			go.GetComponent<Dragging_Slot> ().newImg = itemSprite;
			go.GetComponent<Dragging_Slot> ().ds_item = slotItem;
		} else {
			print ("No item = no drag");
		}
	}

	public virtual void ItemDrop (Item i) {
		if (!p_inv.InventoryCheck(i)) {
//			int slotQuantity = i.slot.GetComponent<INV_Slot> ().itemQuantity;
			i.slot.GetComponent<INV_Slot> ().EmptyItemSlot ();
			p_inv.AddItemToInventory (i, itemQuantity);
		} 
	}

	public void SpawnItemOptionPanel () {
		GameObject go = Instantiate (itemOptionPanel,transform.position, Quaternion.identity,p_inv.inventoryPanel.transform);
		go.transform.localScale = Vector3.one;
		go.GetComponent<Item_Option_Panel> ().spawningSlot = this.gameObject;
	}
}
