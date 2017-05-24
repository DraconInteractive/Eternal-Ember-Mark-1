using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Item", menuName = "Generate/Item/Base", order = 1)]
public class Item : ScriptableObject {

	public string itemName;
	public Sprite itemSprite;
	public GameObject slot;

//	public Item (string i_name, int i_quantity, Sprite i_sprite) {
//		itemName = i_name;
//		itemQuantity = i_quantity;
//		itemSprite = i_sprite;
//	}

	public bool isInWeaponSlot;


}
