using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField] private bool facingRight = true;
	[SerializeField] private int playerSpeed;
	[SerializeField] private int playerJumpPower;
    [SerializeField] private float moveX;
    [SerializeField] private int maxJumps;
    [SerializeField] private int maxDashes;
    [SerializeField] private float dashDistance;
    [SerializeField] private float dashSpeed;

    //Rigidbody used for gravity and such
    private Rigidbody2D playerRB;
    private Animator playerAnim;

	//Start the player with full lives and tries
	private int lives = 3;
	private int tries = 1;

    //Used to check how many jumps have been done before resetting
    private int jumps;
    private int dashes;

    //Variables used for dashing mechanic
    private Vector3 dashPOS;
    public bool isDashing = false;
    public bool isHiding = false;

	private void Start()
	{
        playerRB = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
	}

	private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Floor")
        {
            //playerAnim.SetBool("isGrounded", true);
            jumps = 0;
            dashes = 0;
            Debug.Log("Touching Floor");
        }
        else if (col.gameObject.tag == "Platform")
        {
            //playerAnim.SetBool("isGrounded", true);
            jumps = 0;
            dashes = 0;
            Debug.Log("Touching Platform");
        }
        else if (col.gameObject.tag == "Lava") 
        {
            Debug.Log("Touching Lava!");
        }
        else {
            //playerAnim.SetBool("isGrounded", false);
        }
    }

    // Update is called once per frame
    private void Update () {
        PlayerMove();
	}

	private void PlayerMove() {

        if (!isHiding)
        {
            //Regular Player controls
            if (!isDashing)
            {
                playerRB.bodyType = RigidbodyType2D.Dynamic;

                moveX = Input.GetAxisRaw("Horizontal");

                //If player is moving at all, animate
                if (moveX != 0f)
                {
                    StartRunning(true);
                }
                else
                {
                    StartRunning(false);
                }

                if(Input.GetKeyDown(KeyCode.Space) || playerRB.velocity.y != 0f){
                    playerAnim.SetBool("isGrounded", false);
                }
                else {
                    playerAnim.SetBool("isGrounded", true);
                }

                //Jump controls
                if (Input.GetButtonDown("Jump"))
                {
                    Jump();
                    jumps = jumps + 1;
                }

                //Dash controls
                if (Input.GetKeyDown(KeyCode.LeftShift) && (dashes < maxDashes))
                {
                    dashPOS = GetDashPosition();
                    isDashing = true;
                    dashes++;
                }

                //Direction the player is facing
                if (Input.GetAxisRaw("Horizontal") < 0 && facingRight)
                {
                    FlipPlayer();
                }
                else if (Input.GetAxisRaw("Horizontal") > 0 && !facingRight)
                {
                    FlipPlayer();
                }

                //Physics (Super basic way of getting the character moving, we can mess with it as we go forward)
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);

            }

            //Player cannot add input until dash has ended
            else
            {
                playerRB.bodyType = RigidbodyType2D.Static;

                if ((transform.position.x >= dashPOS.x && facingRight) || (transform.position.x <= dashPOS.x && !facingRight))
                {
                    isDashing = false;
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, dashPOS, dashSpeed * Time.deltaTime);
                }
            }
        }

	}

	private void Jump() {
        // Controls for the double jump (Glide later)
        if(jumps < maxJumps )
        {
            GetComponent<Rigidbody2D>().velocity = (Vector2.up * playerJumpPower);
            print(jumps);
        }
	}

	private void FlipPlayer() {
		facingRight = !facingRight;
		Vector2 localScale = gameObject.transform.localScale;
		localScale.x *= -1;
		transform.localScale = localScale;
	}

	private void HurtPlayer() {
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

    private Vector3 GetDashPosition() 
    {
        //Dash to the right
        if (facingRight)
        {
            Vector3 newPosition = new Vector3(transform.position.x + dashDistance, transform.position.y);
            return newPosition;
        }
        //Dash to the left
        else
        {
            Vector3 newPosition = new Vector3(transform.position.x - dashDistance, transform.position.y);
            return newPosition;
        }
    }

    private void StartRunning(bool state)
    {
        playerAnim.SetBool("isRunning", state);
    }
}
