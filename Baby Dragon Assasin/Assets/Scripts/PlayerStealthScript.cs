using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStealthScript : MonoBehaviour {

	private bool isHiding;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D coll) 
	{
		if (coll.tag == "Cover") {
			Debug.Log("I can hide here!");
		}
	}
}
