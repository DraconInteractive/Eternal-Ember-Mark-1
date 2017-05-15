using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peasant : MonoBehaviour {

	public Color robesColour;

	public Renderer robesRenderer;

	public bool chooseRandom;

	void Start () {
		if (chooseRandom) {
			robesRenderer.material.color = new Color (Random.value, Random.value, Random.value);
		} else {
			robesRenderer.material.color = robesColour;
		}

	}
}
