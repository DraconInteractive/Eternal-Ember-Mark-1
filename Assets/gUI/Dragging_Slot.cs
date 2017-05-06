using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Dragging_Slot : MonoBehaviour {
	[HideInInspector]
	public INV_Slot initSlot;
	public Item ds_item;
	public Image img;
	[HideInInspector]
	public Sprite newImg;
	void Start () {
		img.sprite = newImg;
	}
	// Update is called once per frame
	void Update () {
		transform.position = Input.mousePosition;
		if (Input.GetMouseButtonUp(0)) {
			List<RaycastResult> hitList = new List<RaycastResult> ();
//			Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
//			hits = Physics.RaycastAll (camRay);
			var pointer = new PointerEventData (EventSystem.current);
			pointer.position = Input.mousePosition;
			EventSystem.current.RaycastAll (pointer, hitList);
//			EventSystem.current.RaycastAll(Pointer)
			for (int i = 0; i < hitList.Count; i++) {
				GameObject g = hitList [i].gameObject;
				print (g.name);
				if (g.GetComponent<INV_Slot>()) {
					print ("FOUND SLOT");
					g.GetComponent<INV_Slot> ().ItemDrop ((ds_item as EquipmentItem));
					break;
				}
			}

			Destroy (this.gameObject);
		}
	}
}
