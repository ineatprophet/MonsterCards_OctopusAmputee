﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class DragPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler {
	
	public Vector2 pointerOffset;
	public GameObject CardA;
	public GameObject CardB;

	public RectTransform canvasRectTransform;
	public RectTransform panelRectTransform;
	public RectTransform otherPanelRectTransform;
	public RectTransform initialRectTransform;

	public float initialW;
	public float initialH;

	Vector2 initialMouse;
	Vector3 initialScale;

	//initialize variables
	void Awake () {	
		Canvas canvas = GetComponentInParent <Canvas>();
		if (canvas != null) {
			canvasRectTransform = canvas.transform as RectTransform;
		} else
			Debug.Log ("Canvas is null");
	}

	public void TestFunction(int number){
		Debug.Log ("Card" + number + " was clicked.");
	}

	public void BringToFront(GameObject card){
		card.transform.SetAsLastSibling ();	
	}

	public void SetActiveCard(GameObject card){
		initialMouse = Input.mousePosition;
		CardA = card;
		Debug.Log (CardA + " was clicked.");
		GameObject[] cardlist = GameObject.FindGameObjectsWithTag ("Card");
		foreach (GameObject i in cardlist) {
			if (i != card){
				CardB = i;
			}
		}
		panelRectTransform = (RectTransform)CardA.transform;
		otherPanelRectTransform = (RectTransform)CardB.transform;
		Vector3 newScale = new Vector3 (1.1f, 1.1f, 1.1f); //enlarge factor
		initialScale = panelRectTransform.localScale;
		panelRectTransform.localScale = newScale; //enlarge the selection
	}

	public void UIDrag (GameObject card){
		Debug.Log (CardA + " is being dragged.");
		Vector2 deltaMouse;
		Vector2 currentMouse = Input.mousePosition;
		deltaMouse = initialMouse - currentMouse;
		deltaMouse.y = 0; //locks things to the x axis

		panelRectTransform.localPosition = (Vector2)panelRectTransform.localPosition - deltaMouse;
		otherPanelRectTransform.localPosition = (Vector2)otherPanelRectTransform.localPosition + deltaMouse;

		initialMouse = Input.mousePosition;

	}

	public void TwoCardClamp (){
		//take the incoming mouse position, compare the pointer with the location of the cards, and keep everything within bounds.
		//Determine the positions of the cards. LeftCard cannot go further left. RightCard cannot go further right. Neither card can cross the middle boundary.
	}

	public void UIEndDrag(GameObject card){
		panelRectTransform.localScale = initialScale;
	}
	
	public void OnPointerDown (PointerEventData data) {
	}


	public void OnPointerUp (PointerEventData data) {
	}
	
	public void OnDrag (PointerEventData data) {
	}

	//constrains pointer positions
	Vector2 ClampToWindow (PointerEventData data) {
		Vector2 rawPointerPosition = Input.mousePosition;
		
		Vector3[] canvasCorners = new Vector3[4];
		canvasRectTransform.GetWorldCorners (canvasCorners);
		
		float clampedX = Mathf.Clamp (rawPointerPosition.x, canvasCorners[0].x, canvasCorners[2].x);
		float clampedY = Mathf.Clamp (rawPointerPosition.y, Screen.height/2, Screen.height/2);

		
		Vector2 newPointerPosition = new Vector2 (clampedX, clampedY);
		return newPointerPosition;
	}
}