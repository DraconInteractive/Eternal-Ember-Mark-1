using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Item", menuName = "Generate/Item/Equipment", order = 1)]
public class EquipmentItem : Item {

	public GameObject itemPrefab;
	public enum equipmentType {Weapon, Offhand, Helm, Armour, Necklace, Earring};
	public equipmentType itemType;
	public float itemPower;
}
