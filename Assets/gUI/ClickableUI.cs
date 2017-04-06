using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickableUI : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler {

	public bool isInvSlot, isInvOptPanel, isSpellIcon;

	Spell_Icon s_i;
	INV_Slot inv_s;
	Item_Option_Panel i_o_p;
	void Start () {
		if (isInvSlot) {
			inv_s = GetComponent<INV_Slot> ();
		}
		if (isInvOptPanel) {
			i_o_p = GetComponent<Item_Option_Panel> ();
		}
		if (isSpellIcon) {
			s_i = GetComponent<Spell_Icon> ();
		}
	}
	public void OnPointerClick(PointerEventData eventData) {
		
		if (eventData.button == PointerEventData.InputButton.Right) {
			if (isInvSlot) {
				inv_s.RightClick ();
			} else if (isInvOptPanel) {
				i_o_p.stopDestroy = true;
			}
		} else if (eventData.button == PointerEventData.InputButton.Left) {
			if (isInvOptPanel) {
				i_o_p.stopDestroy = true;
			} else if (isSpellIcon) {
				s_i.MouseClick ();
			}
		}
	}

	public void OnPointerDown (PointerEventData eventData) {
		
		if (eventData.button == PointerEventData.InputButton.Left) {
			if (isInvSlot) {
				inv_s.LeftClick ();
				//				print (GetComponent<INV_Slot>().itemQuantity);
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