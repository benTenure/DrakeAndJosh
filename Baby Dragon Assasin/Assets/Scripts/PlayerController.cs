using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField] private int playerSpeed = 10;
	[SerializeField] private bool facingRight = true;
	[SerializeField] private int playerJumpPower = 1000;
	[SerializeField] private float moveX;

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
}
