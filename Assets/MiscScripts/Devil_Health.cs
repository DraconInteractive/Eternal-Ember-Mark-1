using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devil_Health : Health {
	Animator anim;



	public override void Damage (float damage, GameObject initiator)
	{
		anim = GetComponent<Devil_Enemy> ().anim;
		base.Damage (damage, initiator);
		if (anim != null) {
			anim.SetTrigger ("GetHit");
		}
		print ("GetDamaged");
	}

	public override void Die ()
	{
		base.Die ();
		anim.SetTrigger ("Die");

	}
}
