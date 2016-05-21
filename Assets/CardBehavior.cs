using UnityEngine;
using System.Collections;

public class CardBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void ThisCardWins()
	{
		this.GetComponent<CardStats>().wins += 1;
	}
}
