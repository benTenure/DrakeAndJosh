using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour {

	public HelathSystemScript playerHealth;
	public float restartDelay = 5f;

	private float restartTimer;

	// Update is called once per frame
	void Update () {
		if (playerHealth.currentHealth <= 0) 
		{
			Debug.Log("Counting down...");
			restartTimer += Time.deltaTime;

			if (restartTimer >= restartDelay)
			{
				SceneManager.LoadScene("Level01");
			}
		}		
	}
}
