using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Spell", menuName = "Generate/Spell", order = 1)]
public class Spell : ScriptableObject {

	public string s_name, s_desc;

	public enum spellType {Buff, Debuff, Conjuration, Attack, Action};
	public spellType thisSpellType;

	public bool unlocked, learned;

	public virtual void CastSpell () {
		
	}

	public virtual void LearnSpell () {
		learned = true;
	}

	public virtual void UnlockSpell () {
		unlocked = true;
	}
}
