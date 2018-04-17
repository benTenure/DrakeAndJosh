using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour {

	public HelathSystemScript playerHealth;
    public GameObject player;
	public float restartDelay = 5f;

	private float restartTimer;

	void Update () {

        //Restarts level when player dies
		if (playerHealth.currentHealth <= 0) 
		{
            RestartLevel();
		}

        if (player.transform.position.y <= -10) {
            RestartLevel();
        }
	}

    void RestartLevel() {
        Debug.Log("Counting down...");
        restartTimer += Time.deltaTime;

        if (restartTimer >= restartDelay)
        {
            SceneManager.LoadScene("Level01");
        }
    }
}
