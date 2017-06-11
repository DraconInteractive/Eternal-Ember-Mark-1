using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : Interactable {

	public int itemQuantity;

	public override void Interact(){
		Destroy (this.gameObject);
	}

}
