using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : Enemy {

	public Coroutine co;
	public bool active;
	public override void Damage ()
	{
		base.Damage ();
		if (co == null) {
			co = StartCoroutine (DamageRoutine ());
		}
	}

	IEnumerator DamageRoutine () {
		active = true;
		MeshRenderer m = GetComponent<MeshRenderer> ();
		Color c = m.material.color;
		m.material.color = Color.red;
		yield return new WaitForSeconds (0.5f);
		m.material.color = c;
		co = null;
		active = false;
		yield break;
	}
}
