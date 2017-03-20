using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Item", menuName = "Generate/Item/Consumable", order = 1)]
public class ConsumableItem : Item {

	public enum consumableType {Food, Drink, Potion, Scroll};
	public consumableType itemType;

	public enum effectType {HealthRestore, HealthRegen, ManaRestore, ManaRegen, Poison};
	public effectType itemEffect;

	public float itemPower;
	public virtual void ConsumableEffect () {
		switch (itemEffect)
		{
		case effectType.HealthRestore:
			Player.player.gameObject.GetComponent<Health> ().Heal (itemPower);
			break;
		case effectType.HealthRegen:
			Player.player.gameObject.GetComponent<Health> ().ProcRegen (itemPower * Time.deltaTime, 2);
			break;
		case effectType.ManaRestore:
			break;
		case effectType.ManaRegen:
			break;
		case effectType.Poison:
			Player.player.gameObject.GetComponent<Health> ().ProcPoison (itemPower * Time.deltaTime, 2);
			break;
		}

		if (slot != null) {
			INV_Slot thisSlot = slot.GetComponent<INV_Slot> ();
			thisSlot.itemQuantity--;
			if (thisSlot.itemQuantity <= 0) {
				thisSlot.EmptyItemSlot ();
			} else {
				if (thisSlot.newItem) {
					thisSlot.SetNewItem (false);
				}
			}
		} else {
			Debug.Log ("Test");
		}
//		if (slot.GetComponent<INV_Slot>().itemQuantity <= 0) {
//			
//		}
	}
}
