using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderButton : MonoBehaviour, ICanvasRaycastFilter{
	RectTransform rectTransform;
	Collider2D myCollider;

	void Start(){
		rectTransform = gameObject.GetComponent<RectTransform>();
		myCollider = gameObject.GetComponent<Collider2D>();
	}

	void Update(){
		
	}

	public bool IsRaycastLocationValid(Vector2 screenPos, Camera eventCamera){
		Vector3 worldPoint = Vector3.zero;
		bool isInside = RectTransformUtility.ScreenPointToWorldPointInRectangle(
			rectTransform, screenPos, eventCamera, out worldPoint);
		if (isInside){
			isInside = myCollider.OverlapPoint(worldPoint);
		}
		return isInside;
	}
}
