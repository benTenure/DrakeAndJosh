using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStealthScript : MonoBehaviour {

	private bool isHiding;
    private bool canHide;
    private SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
        if (canHide) {
            if (Input.GetKeyDown(KeyCode.E)) {
                
            }
            else {
                
            }
        }

	}

	void OnTriggerEnter2D(Collider2D coll) 
	{
		if (coll.tag == "Cover") {
            canHide = true;
		}
	}

	private void OnTriggerExit2D(Collider2D coll)
	{
        if (coll.tag == "Cover") {
            canHide = false;
        }	
	}
}
