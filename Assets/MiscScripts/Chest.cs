using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable {

	public GameObject chestLid;

	public bool isOpen;

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
