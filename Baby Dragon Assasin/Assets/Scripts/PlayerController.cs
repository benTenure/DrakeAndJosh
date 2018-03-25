using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField] private bool facingRight = false;
	[SerializeField] private int playerSpeed = 10;
	[SerializeField] private int playerJumpPower = 1000;
	[SerializeField] private float moveX;

	//Start the player with full lives and tries
	private int lives = 3;
	private int tries = 1;
	private float dashDistance = 5f;
	private float dashVelocity = 1f;

    public int maxJumps = 1;

    int jumps;

	//Changes that don't matter!

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Resetting Jumps");
        jumps = 0;

        if (col.gameObject.tag == "Floor")
        {
            Debug.Log("Touching Floor");
        }
        else if (col.gameObject.tag == "Platform")
        {
            Debug.Log("Touching Platform");
        }
        else if (col.gameObject.tag == "Lava") 
        {
            Debug.Log("Touching Lava!");
        }
    }

    // Update is called once per frame
    void Update () {
		PlayerMove();
	}

	void PlayerMove() {
		//Controls
		moveX = Input.GetAxis("Horizontal");

		if(Input.GetButtonDown("Jump")) {
			Jump();
            jumps = jumps + 1;
		}
//		if(Input.GetButtonDown("Shift"))
//		{
//			Vector3 newPosition = transform.position;
//			
//		}

		//Direction the player is facing
		if (moveX < 0.0f && !facingRight) {
			FlipPlayer();
		} else if (moveX > 0.0f && facingRight) {
			FlipPlayer();
		}

		//Physics (Super basic way of getting the character moving, we can mess with it as we go forward)
		gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);


	}

	void Jump() {
        // Controls for the double jump (Glide later)
        if(jumps < maxJumps )
        {
            GetComponent<Rigidbody2D>().velocity = (Vector2.up * playerJumpPower);
            print(jumps);
        }
	}

	void FlipPlayer() {
		facingRight = !facingRight;
		Vector2 localScale = gameObject.transform.localScale;
		localScale.x *= -1;
		transform.localScale = localScale;
	}

	void HurtPlayer() {
		if (tries > 0 && lives > 0)
		{
			//Hurt the player.
			tries--;
			//Code that will bounce the player in the opposite direction of what hurt him.
		}
		else if (tries == 0 && lives > 0)
		{
			//Take away a life and reset the player's tries
			lives--;
			tries = 1;
			//Code that will reset player to most recent checkpoint
		}
		else {
			//Code that will trigger a game over screen
		}
	}
}
