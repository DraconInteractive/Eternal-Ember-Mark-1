using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName="Rune", menuName="Generate/Rune/WeaponAttack")]
public class WeaponAttack : Rune {

	public enum wa_type {Ignis, Terra, Luma, Voll};
	public wa_type rune_type;

	public float damage, duration;
	public float powerMod;

	public override void RuneEffect (GameObject target)
	{
		base.RuneEffect (target);

		powerMod = 1;
		if (RuneControl.rune_control.runeTwo.rune_name == "Ra") {
			powerMod = 1.5f;
		}
		if (target.tag == "Enemy") {
			switch (rune_type)
			{
			case wa_type.Ignis:
				target.GetComponent<Health> ().ProcPoison (damage * powerMod * Time.deltaTime, duration * powerMod, Health.poisonType.Fire, Player.player.gameObject);
				break;
			case wa_type.Terra:
				target.GetComponent<Enemy> ().ProcSlow (duration * powerMod);
				break;
			case wa_type.Luma:
				//Stun Enemy
				target.GetComponent<Enemy> ().ProcStun (duration * powerMod);
				break;
			case wa_type.Voll:
				//Some type of loot thing
				target.GetComponent<Health> ().Damage (damage * powerMod, Player.player.gameObject);
				break;
			}
		}

	}
}
