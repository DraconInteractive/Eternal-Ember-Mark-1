using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	Vector3 startPos;
	public void OnBeginDrag (PointerEventData eventData) {
		startPos = transform.position;
		print ("Begin Drag");
	}

	public void OnDrag (PointerEventData eventData) {
		transform.position = Input.mousePosition;
	}

	public void OnEndDrag (PointerEventData eventData) {
		transform.position = startPos;
	}
}
