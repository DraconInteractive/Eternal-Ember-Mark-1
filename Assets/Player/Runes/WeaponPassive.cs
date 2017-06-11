using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName="Rune", menuName="Generate/Rune/WeaponPassive")]
public class WeaponPassive : Rune {

	public enum wp_type {Aqua, Aether, Gaia, Ra, Giss};
	public wp_type rune_type;

	public float rune_power;
	[HideInInspector]
	public float powerMod;
	public override void RuneEffect (GameObject target)
	{
		base.RuneEffect (target);

		switch (rune_type)
		{
		case wp_type.Aqua:
			//Add health
			break;
		case wp_type.Aether:
			//Add Speed
			break;
		case wp_type.Gaia:
			//Increase time before wild animals attack
			break;
		case wp_type.Ra:
			//Amplify other runes
			break;
		case wp_type.Giss:
			//Terrain movement helper
			break;
		}
	}

	public void NullEffect () {
		switch (rune_type)
		{
		case wp_type.Aqua:
			//Add health
			break;
		case wp_type.Aether:
			//Add Speed
			break;
		case wp_type.Gaia:
			//Increase time before wild animals attack
			break;
		case wp_type.Ra:
			//Amplify other runes
			break;
		case wp_type.Giss:
			//Terrain movement helper
			break;
		}
	}
}
