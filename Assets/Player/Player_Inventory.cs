using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inventory : MonoBehaviour {

	public static Player_Inventory p_inventory;
	PossessionController pc_controller;
	Player player;
	Player_Camera p_camera;
	Player_Movement p_movement;
	Player_UI p_UI;

	public GameObject inventoryPanel, slotPanel, equipmentPanel;

	public List<INV_Slot> invSlots = new List<INV_Slot>();
	public EQU_Slot helmSlot, armourSlot, weaponSlot, offhandSlot, earOneSlot, earTwoSlot, neckSlot;

	public bool inventoryOpen;

	Coroutine toggleRoutine;

	public List<Item> initialItems;
	// Use this for initialization
	void Awake () {
		p_inventory = GetComponent<Player_Inventory> ();
		invSlots.AddRange(slotPanel.GetComponentsInChildren<INV_Slot> ());
	}

	void Start () {
		player = Player.player;
		p_camera = Player_Camera.p_camera;
		p_movement = Player_Movement.p_movement;
		p_UI = Player_UI.p_UI;
		pc_controller = PossessionController.pc_controller;
		ToggleInventory (false);

		if (initialItems.Count > 0) {
			StartCoroutine (RecieveInitialItems ());
		}
	}

	IEnumerator RecieveInitialItems () {
		for (int i = 0; i < initialItems.Count; i++) {
			AddItemToInventory (initialItems [i], 1);
			yield return null;
		}
		yield break;
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.I)) {
			ToggleInventory (!inventoryOpen);
		}
	}

	public void ToggleInventory (bool state) {
		if (pc_controller.currentHost != PossessionController.hostType.PLAYER && state == true) {
			return;
		}
		if (state) {
			if (!p_movement.canMove) {
				return;
			}
			if (toggleRoutine == null) {
				toggleRoutine = StartCoroutine (Open ());
			}
		} else {
			
			if (toggleRoutine == null) {
				toggleRoutine = StartCoroutine (Close ());
			}
		}
		player.ToggleCursor (state);

	}

	IEnumerator Open () {
//		p_camera.invActive = true;
//		yield return null;
//		while (p_camera.atTarget == false) {
//			if (Input.GetKeyDown(KeyCode.I)) {
//				StartCoroutine (Close ());
//				yield break;
//			}
//			yield return null;
//		}
		Player_Movement.p_movement.canMove = false;
		p_camera.invActive = true;
		yield return null;
		p_camera.INVInitiator ();
		p_movement.canMove = false;
		while (!p_camera.atTarget) {
			if (Input.GetKeyDown(KeyCode.I)) {
				StartCoroutine (Close ());
				yield break;
			}
			yield return null;
		}

//		p_camera.GetComponent<Camera> ().enabled = false;
//		p_camera.transform.GetChild (0).gameObject.GetComponent<Camera> ().clearFlags = CameraClearFlags.Skybox;
		inventoryPanel.SetActive (true);
		inventoryOpen = true;
		toggleRoutine = null;
		yield break;
	}

	IEnumerator Close () {
		Player_Movement.p_movement.canMove = true;
		p_camera.atTarget = false;
		p_camera.invActive = false;
//		p_camera.GetComponent<Camera> ().enabled = true;
//		p_camera.transform.GetChild (0).gameObject.GetComponent<Camera> ().clearFlags = CameraClearFlags.Depth;
		inventoryPanel.SetActive (false);
		inventoryOpen = false;
		p_movement.canMove = true;
		toggleRoutine = null;
		yield break;
	}

	public bool InventoryCheck (Item item) {
		List<INV_Slot> currentINVItems = new List<INV_Slot> ();

		//Retrive Full slots
		for (int i = 0; i < invSlots.Count; i++) {
			if (invSlots[i].hasItem) {
				currentINVItems.Add (invSlots [i]);
			}
		}

		for (int i = 0; i < currentINVItems.Count; i++) {
			if (currentINVItems[i].slotItem == item) {
				return true;
			}
		}

		return false;

	}

	/// <summary>
	/// Adds the item to inventory directly.
	/// </summary>
	/// <param name="i">The index.</param>
	public void AddItemToInventory (Item item, int quantity) {


//		//Search for item. If found, add to quantity, then return.

//		for (int i = 0; i < currentINVItems.Count; i++) {
//			if (currentINVItems[i].slotItem.itemName == item.itemName) {
//				currentINVItems [i].itemQuantity += quantity;
//				itemInINV = true;
//				break;
//			}
//		}
		bool itemInINV = InventoryCheck(item);

		if (itemInINV) {
			item.slot.GetComponent<INV_Slot> ().itemQuantity += quantity;
			p_UI.SpawnEventText (item.itemName + " added to inventory");
			return;
		}

		//Otherwise, we now search for an empty slot
		bool emptySlotFound = false;

		for (int i = 0; i < invSlots.Count; i++) {
			if (invSlots[i].hasItem == false) {
				invSlots [i].RecieveItem (item, quantity);
				emptySlotFound = true;
				break;
			}
		}

		if (emptySlotFound) {
			p_UI.SpawnEventText (item.itemName + " added to inventory");
			return;
		} else {
			p_UI.SpawnEventText ("Unable to add " + item.itemName + " to inventory");
		}
	}
		
	public void EquipItem (EquipmentItem item) {
		switch (item.itemType)
		{
		case EquipmentItem.equipmentType.Weapon:
			if (weaponSlot.hasItem) {
				if (item.slot != null) {
					Item currentWeapon = weaponSlot.slotItem;
					Item futureWeapon = item;

					INV_Slot cwSlot = currentWeapon.slot.GetComponent<INV_Slot> ();
					INV_Slot fwSlot = futureWeapon.slot.GetComponent<INV_Slot> ();

					cwSlot.EmptyItemSlot ();
					fwSlot.EmptyItemSlot ();
					cwSlot.RecieveItem (futureWeapon, 1);
					fwSlot.RecieveItem (currentWeapon, 1);
				}
			} else {
				if (item.slot != null) {
					item.slot.GetComponent<INV_Slot> ().EmptyItemSlot ();
				} 
				weaponSlot.RecieveItem (item, 1);
			}
			if (player.playerWeapon != null) {
				player.playerWeapon.UnEquip ();
			}
			GameObject newWeapon = Instantiate (item.itemPrefab) as GameObject;
			Weapon w = newWeapon.GetComponent<Weapon> ();
			w.Equip ();
			break;
		case EquipmentItem.equipmentType.Offhand:
			if (offhandSlot.hasItem) {
				if (item.slot != null) {
					Item currentOffhand = offhandSlot.slotItem;
					Item futureOffhand = item;

					INV_Slot coSlot = currentOffhand.slot.GetComponent<INV_Slot> ();
					INV_Slot foSlot = futureOffhand.slot.GetComponent<INV_Slot> ();

					coSlot.EmptyItemSlot ();
					foSlot.EmptyItemSlot ();
					coSlot.RecieveItem (currentOffhand, 1);
					foSlot.RecieveItem (futureOffhand, 1);
				}
			} else {
				if (item.slot != null) {
					item.slot.GetComponent<INV_Slot> ().EmptyItemSlot ();
				}
				offhandSlot.RecieveItem (item, 1);
			}
			if (player.playerShield != null) {
				player.playerShield.UnEquip ();
			}
			GameObject newShield = Instantiate (item.itemPrefab) as GameObject;
			Shield s = newShield.GetComponent<Shield> ();
			s.Equip ();

			break;
		case EquipmentItem.equipmentType.Armour:
			if (armourSlot.hasItem) {
				if (item.slot != null) {
					Item currentArmour = armourSlot.slotItem;
					Item futureArmour = item;

					INV_Slot caSlot = currentArmour.slot.GetComponent<INV_Slot> ();
					INV_Slot faSlot = futureArmour.slot.GetComponent<INV_Slot> ();

					caSlot.EmptyItemSlot ();
					faSlot.EmptyItemSlot ();
					caSlot.RecieveItem (futureArmour, 1);
					faSlot.RecieveItem (currentArmour, 1);
				}
			} else {
				if (item.slot != null) {
					item.slot.GetComponent<INV_Slot> ().EmptyItemSlot();
				}

				armourSlot.RecieveItem (item, 1);
			}
			break;
		case EquipmentItem.equipmentType.Helm:
			if (helmSlot.hasItem) {
				if (item.slot != null) {
					Item currentHelm = helmSlot.slotItem;
					Item futureHelm = item;

					INV_Slot chSlot = currentHelm.slot.GetComponent<INV_Slot> ();
					INV_Slot fhSlot = futureHelm.slot.GetComponent<INV_Slot> ();

					chSlot.EmptyItemSlot ();
					fhSlot.EmptyItemSlot ();
					chSlot.RecieveItem (currentHelm, 1);
					fhSlot.RecieveItem (futureHelm, 1);
				}
			} else {
				if (item.slot != null) {
					item.slot.GetComponent<INV_Slot> ().EmptyItemSlot();
				}

				helmSlot.RecieveItem (item, 1);
			}
			break;
		case EquipmentItem.equipmentType.Necklace:
			if (neckSlot.hasItem) {
				if (item.slot != null) {
					Item currentNeck = neckSlot.slotItem;
					Item futureNeck = item;

					INV_Slot cnSlot = currentNeck.slot.GetComponent<INV_Slot> ();
					INV_Slot fnSlot = futureNeck.slot.GetComponent<INV_Slot> ();

					cnSlot.EmptyItemSlot ();
					fnSlot.EmptyItemSlot ();
					cnSlot.RecieveItem (currentNeck, 1);
					fnSlot.RecieveItem (futureNeck, 1);
				}
			} else {
				if (item.slot != null) {
					item.slot.GetComponent<INV_Slot> ().EmptyItemSlot();
				}

				neckSlot.RecieveItem (item, 1);
			}
			break;
		}
		p_UI.SpawnEventText (item.itemName + " equipped on player");
	}

	public void EquipItem (EquipmentItem item, int earSlotNum) {
		
		switch (earSlotNum)
		{
		case 1:
			if (earOneSlot.hasItem) {
				if (item.slot != null) {
					Item currentEarring = earOneSlot.slotItem;
					Item futureEarring = item;

					INV_Slot ceSlot = currentEarring.slot.GetComponent<INV_Slot> ();
					INV_Slot feSlot = futureEarring.slot.GetComponent<INV_Slot> ();

					ceSlot.EmptyItemSlot ();
					feSlot.EmptyItemSlot ();
					ceSlot.RecieveItem (currentEarring, 1);
					feSlot.RecieveItem (futureEarring, 1);
				}
			} else {
				if (item.slot != null) {
					item.slot.GetComponent<INV_Slot> ().EmptyItemSlot ();
				}

				earOneSlot.RecieveItem (item, 1);
			}
			break;
		case 2:
			if (earTwoSlot.hasItem) {
				if (item.slot != null) {
					Item currentEarring = earTwoSlot.slotItem;
					Item futureEarring = item;

					INV_Slot ceSlot = currentEarring.slot.GetComponent<INV_Slot> ();
					INV_Slot feSlot = futureEarring.slot.GetComponent<INV_Slot> ();

					ceSlot.EmptyItemSlot ();
					feSlot.EmptyItemSlot ();
					ceSlot.RecieveItem (currentEarring, 1);
					feSlot.RecieveItem (futureEarring, 1);
				}
			} else {
				if (item.slot != null) {
					item.slot.GetComponent<INV_Slot> ().EmptyItemSlot ();
				}

				earTwoSlot.RecieveItem (item, 1);
			}
			break;
		}
			
		p_UI.SpawnEventText (item.itemName + " equipped on player");
	}
}
