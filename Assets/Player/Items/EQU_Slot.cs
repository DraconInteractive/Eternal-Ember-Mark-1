using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EQU_Slot : INV_Slot {

	Color slotImageInitColour;


	public int earringSLotN;
	public override void SlotStart ()
	{
		base.SlotStart ();
		slotImageInitColour = slotImage.color;
	}

	public override void UseSlot ()
	{
		if (slotItem != null) {
			Item i = slotItem;
			EmptyItemSlot ();
			p_inv.AddItemToInventory (i, 1);
			if (slotItem is EquipmentItem) {

				switch ((slotItem as EquipmentItem).itemType)
				{
				case EquipmentItem.equipmentType.Weapon:
					Player.player.playerWeapon.UnEquip ();
					break;
				case EquipmentItem.equipmentType.Offhand:
					Player.player.playerShield.UnEquip ();
					break;
				}
			}

			//			EmptyItemSlot ();
		}
	}
	public override void RightClick ()
	{
		

	}

	public override void RecieveItem (Item item, int quantity)
	{
		hasItem = true;

		itemName = item.itemName;
		itemQuantity = quantity;

		if (item.itemSprite != null) {
			itemSprite = item.itemSprite;
			slotImage.sprite = itemSprite;
			slotImage.color = new Color (1, 1, 1, 1);
		}

		slotItem = item;

		item.slot = this.gameObject;
	}

	public override void EmptyItemSlot ()
	{
		base.EmptyItemSlot ();
		slotImage.color = slotImageInitColour;
	}

	public override void ItemDrop (Item i)
	{
		if (i != null) {
			if (i is EquipmentItem) {
				print ("!NULL+EQUI");
				if ((i as EquipmentItem).itemType == EquipmentItem.equipmentType.Earring) {
					p_inv.EquipItem ((i as EquipmentItem), earringSLotN);
				} else {
					p_inv.EquipItem ((i as EquipmentItem));
				}

			} else {
				print ("!NULL+!EQUI");
			}
		} else {
			print ("NULL");
		}

	}
}
