using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName="Rune", menuName="Generate/Rune")]
public class Rune : ScriptableObject{
	public string rune_name;

	public string rune_description;

	public int rune_tier;

	public Sprite rune_sprite;

	public enum runeGrammar {Essence, Conjunction, Use};
	public runeGrammar rune_grammar;

	public virtual void RuneEffect (GameObject target) {
		
	}

	/*
	 * 
	 * Runes affect fight animations?
	 * 
	 * 3 Stages
	 * 
	 * Stage One - Essence. 
	 * 
	 * Stage Two - Essence + Action.
	 * 
	 * Stage Three - Essence + Use + Action.
	 * 
	 * Possible Effects
	 * 
	 * Igni - Fire Damage + Dissolve
	 * 
	 * Aqua - Health + Regen
	 * 
	 * Luma - Stunning with light
	 * 
	 * Terra - Slow Enemy on Hit
	 * 
	 * Aether - Increase movement and attack speed
	 */
}
