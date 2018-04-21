using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelathSystemScript : MonoBehaviour {

    //You run out of tries before lives (2 tries gone = 1 life gone)
    public int tries = 3;
    public int lives = 1;

	public int startingHealth = 1;
	public int currentHealth;
	public int playerLives = 1;
    public float thrust = 10f;

    private bool damaged;
	private bool isDead;

    private Rigidbody2D rb2d;

	void Awake() 
	{
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isDead)
		{
			//Debug.Log("Oh no, I died");
			//gameObject.SetActive(false);
			//Debug.Log("Game Over");

            //

		}

        if (Input.GetKeyDown(KeyCode.Q)) {
            TakeDamage();
        }
	}

	//Check for collision
	void OnCollisionEnter2D(Collision2D col) 
	{
		if (col.gameObject.tag == "Lava") 
		{
			Debug.Log("The lava really hurts!");
			TakeDamage();
		}
	}

	public void TakeDamage() 
	{
        //Boolean that is checked in the Update() for flashing red
        damaged = true;

        if (tries >= 1 && !isDead) {

            //Hurt the player
		    tries--;

            //Bounce the player (to the left for now)
            rb2d.AddForce(-transform.right * thrust);

			Debug.Log("Current Health: " + tries);
		}
        else if (tries == 0 && lives >= 0)
		{
			
            isDead = true;
		}
	}

    //private void HurtPlayer() {
    //  if (tries > 0 && lives > 0)
    //  {
    //      //Hurt the player.
    //      tries--;
    //      //Code that will bounce the player in the opposite direction of what hurt him.
    //  }
    //  else if (tries == 0 && lives > 0)
    //  {
    //      //Take away a life and reset the player's tries
    //      lives--;
    //      tries = 1;
    //      //Code that will reset player to most recent checkpoint
    //  }
    //  else {
    //      //Code that will trigger a game over screen
    //  }
    //}

}
