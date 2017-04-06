using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_Option_Panel : MonoBehaviour {
	public bool destroyNextFrame;
	public bool stopDestroy;

	public Button use,drop,kill;

	public GameObject spawningSlot;
	// Use this for initialization
	void Start () {
		destroyNextFrame = false;
		use.onClick.AddListener (() => Use ());
		drop.onClick.AddListener (() => Drop ());
		kill.onClick.AddListener (() => Kill ());
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1)) {
			destroyNextFrame = true;
		} else {
			destroyNextFrame = false;
		}

		if (destroyNextFrame) {
			if (!stopDestroy) {
				Destroy (this.gameObject);
			}
		}
	}

	void Use () {
		print ("Pressed Button");
		spawningSlot.GetComponent<INV_Slot> ().UseSlot ();
	}

	void Drop () {
		print ("Pressed Button");
		spawningSlot.GetComponent<INV_Slot> ().DropItem ();
	}

	void Kill () {
		print ("Pressed Button");
		spawningSlot.GetComponent<INV_Slot> ().DestroyItem ();
	}
	void LateUpdate () {
		stopDestroy = false;
	}

}
