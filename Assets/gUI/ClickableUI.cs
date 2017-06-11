using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickableUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

	public bool isInvSlot, isInvOptPanel, isSpellIcon;

	Spell_Icon s_i;

	void Start () {

		if (isSpellIcon) {
			s_i = GetComponent<Spell_Icon> ();
		}
	
	}
	public void OnPointerClick(PointerEventData eventData) {
		
		if (eventData.button == PointerEventData.InputButton.Left) {
			if (isSpellIcon) {
				s_i.MouseClick ();
			}
		}
	}

	public void OnPointerEnter (PointerEventData eventData) {
		if (isSpellIcon) {
			s_i.MouseEnter ();
		}
	}

	public void OnPointerExit (PointerEventData eventData) {
		if (isSpellIcon) {
			s_i.MouseExit ();
		}
	}
}