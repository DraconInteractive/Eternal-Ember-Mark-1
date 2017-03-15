using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inventory : MonoBehaviour {

	public static Player_Inventory p_inventory;

	Player_Camera p_camera;
	public GameObject inventoryPanel, slotPanel;

	public List<INV_Slot> invSlots = new List<INV_Slot>();

	public bool inventoryOpen;

	Coroutine toggleRoutine;
	// Use this for initialization
	void Awake () {
		p_inventory = GetComponent<Player_Inventory> ();
		invSlots.AddRange(slotPanel.GetComponentsInChildren<INV_Slot> ());

	}

	void Start () {
		p_camera = Player_Camera.p_camera;
		ToggleInventory (false);
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.I)) {
			ToggleInventory (!inventoryOpen);
		}
	}

	public void ToggleInventory (bool state) {
		if (state) {
			if (toggleRoutine == null) {
				toggleRoutine = StartCoroutine (Open ());
			}
		} else {
			if (toggleRoutine == null) {
				toggleRoutine = StartCoroutine (Close ());
			}
		}

	}

	IEnumerator Open () {
		p_camera.invActive = true;
		yield return null;
		while (p_camera.atTarget == false) {
			if (Input.GetKeyDown(KeyCode.I)) {
				StartCoroutine (Close ());
				yield break;
			}
			yield return null;
		}
		inventoryPanel.SetActive (true);
		inventoryOpen = true;
		toggleRoutine = null;
		yield break;
	}

	IEnumerator Close () {
		p_camera.invActive = false;
		inventoryPanel.SetActive (false);
		inventoryOpen = false;
		toggleRoutine = null;
		yield break;
	}

	/// <summary>
	/// Adds the item to inventory directly.
	/// </summary>
	/// <param name="i">The index.</param>
	public void AddItemToInventory (Item item, int quantity) {
		//First check if its in the inventory
		List<INV_Slot> currentINVItems = new List<INV_Slot> ();
		//Retrive Full slots
		for (int i = 0; i < invSlots.Count; i++) {
			if (invSlots[i].hasItem) {
				currentINVItems.Add (invSlots [i]);
			}
		}
		//Search for item. If found, add to quantity, then return.
		bool itemInINV = false;
		for (int i = 0; i < currentINVItems.Count; i++) {
			if (currentINVItems[i].slotItem.itemName == item.itemName) {
				currentINVItems [i].itemQuantity += quantity;
				itemInINV = true;
				break;
			}
		}
		if (itemInINV) {
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
			return;
		} else {
			print ("No Available Slot");
		}
	}
}
