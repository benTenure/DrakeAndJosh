using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	[SerializeField] private bool facingRight = true;
	[SerializeField] private int playerSpeed;
	[SerializeField] private int playerJumpPower;
    [SerializeField] private float moveX;
    [SerializeField] private int maxJumps;
    [SerializeField] private int maxDashes;
    [SerializeField] private float dashDistance;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float idleHeight;
    [SerializeField] private LayerMask hitThisThing;

    //Rigidbody used for gravity and such
    private Rigidbody2D playerRB;
    private Animator playerAnim;

    //Used to check how many jumps have been done before resetting
    private int jumps;
    private int dashes;

    //Variables used for dashing mechanic
    private Vector3 dashPOS;
    public bool isDashing = false;
    public bool isHiding = false;

    private bool movingUp;

	private void Start()
	{
        playerRB = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
	}

	private void OnCollisionEnter2D(Collision2D col)
    {
        //if((col.gameObject.tag == "Floor" || col.gameObject.tag == "Platform"))
        //{
        //    jumps = 0;
        //    dashes = 0;
        //    Debug.Log("Touching Floor");
        //}
        if (col.gameObject.tag == "Win" && playerRB.velocity.y == 0) 
        {
            SceneManager.LoadScene("YouWin");
        }
    }

	private void OnTriggerEnter2D(Collider2D col)
	{
        //Show the "Dash" UI
        if (col.gameObject.name == "Dash") {
            SpriteRenderer quickSR = col.gameObject.GetComponent<SpriteRenderer>();
            quickSR.enabled = true;
        }
        //Show the "Stealth/Hide" UI
        else if (col.gameObject.name == "Stealth") {
            SpriteRenderer quickSR = col.gameObject.GetComponent<SpriteRenderer>();
            quickSR.enabled = true;
        }
	}

	// Update is called once per frame
	private void Update () {
        PlayerMove();
	}

	private void PlayerMove() {

        //Ray for jumping
        RaycastHit hit;
        Ray jumpRay = new Ray(transform.position, Vector3.down);

        if (!isHiding)
        {
            //Regular Player controls
            if (!isDashing)
            {
                playerRB.bodyType = RigidbodyType2D.Dynamic;

                moveX = Input.GetAxisRaw("Horizontal");

                //Debug.DrawRay(transform.position, Vector3.down, Color.red);

                if (IsGrounded())
                {
                    print("resetting jumps...");
                    jumps = 0;
                    dashes = 0;
                }

                //If player is moving at all, animate
                //if (moveX == 1 || moveX == -1)
                if(Mathf.Abs(moveX) > 0)
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
                if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && (dashes < maxDashes))
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
            if (jumps < maxJumps)
            {
                GetComponent<Rigidbody2D>().velocity = (Vector2.up * playerJumpPower);
                //GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpPower, ForceMode2D.Impulse); //Testing new jump
                print(jumps);
            }
	}

	private void FlipPlayer() {
		facingRight = !facingRight;
		Vector2 localScale = gameObject.transform.localScale;
		localScale.x *= -1;
		transform.localScale = localScale;
	}

    private Vector3 GetDashPosition() 
    {
        //UPDATE TO USE A VECTOR POINTING FORWARD TO DETERMINE WHAT TO DO
        //WHEN A WALL APPEARS DURING THE DASH

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

    public bool IsGrounded()
    {

        if (Physics2D.Raycast(this.transform.position, Vector2.down, 1f, hitThisThing.value))
        {
            Debug.Log("Raycast is touching");
            return true;
        }
        else
        {
            return false;
        }
    }
}
