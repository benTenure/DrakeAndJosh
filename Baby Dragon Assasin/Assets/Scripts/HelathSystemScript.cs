using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelathSystemScript : MonoBehaviour {

	public int startingHealth = 2;
	public int currentHealth;
	public int playerLives = 1;

	private bool isDead;

	void Awake() 
	{
		//Start the player at full health
		currentHealth = startingHealth;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isDead)
		{
			Debug.Log("Oh no, I died");
			gameObject.SetActive(false);
			Debug.Log("Game Over");
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

	void TakeDamage() 
	{
		currentHealth--;

		if (currentHealth >= 1 && !isDead) 
		{
			Debug.Log("Current Health: " + currentHealth);
		}
		else
		{
			isDead = true;
		}
	}


}
