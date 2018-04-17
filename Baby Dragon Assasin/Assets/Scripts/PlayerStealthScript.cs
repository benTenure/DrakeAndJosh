using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStealthScript : MonoBehaviour {

    private bool canHide;
    private SpriteRenderer sprite;
    private PlayerController controller;
    private Transform player;
    private Vector3 hideSpot;
    private Vector3 currentSpot;

	// Use this for initialization
	void Awake () {
        sprite = GetComponent<SpriteRenderer>();
        controller = GetComponent<PlayerController>();
        player = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
       
        if (controller.isHiding == false) {
            //Keep track of player's position BEFORE he goes into hiding
            currentSpot = player.position;
        }

        if (canHide) {
            if (Input.GetKeyDown(KeyCode.E))
            {
                HidePlayer();
            }
        }
	}

	void OnTriggerEnter2D(Collider2D coll) 
	{
		if (coll.tag == "Cover") {
            canHide = true;
            //hideSpot = Vector3(coll.transform.position.x, coll.transform.position.y, coll.transform.position.z);
            hideSpot.Set(coll.transform.position.x, coll.transform.position.y, coll.transform.position.z);
		}
	}

	private void OnTriggerExit2D(Collider2D coll)
	{
        if (coll.tag == "Cover") {
            canHide = false;
        }	
	}

    private void HidePlayer() {
        //If the boolean is set to false, the player can no longer be controlled
        controller.isHiding = !controller.isHiding;

        if (controller.isHiding == true) {
            //Move Player to the hiding spot
            player.position = hideSpot;
            sprite.sortingLayerName = "Player Hiding";
        }
        else {
            //Reset player to his original position
            player.position = currentSpot;
            sprite.sortingLayerName = "Player";
        }
    }
}
