using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour {

    public HelathSystemScript playerHealth;
    public GameObject player;
    public GameObject heartR;
    public GameObject heartM;
    public GameObject heartL;
	public float restartDelay = 5f;

	private float restartTimer;
    private Animator animR;
    private Animator animM;
    private Animator animL;

	private void Awake()
	{
        //Probably the worst possible way of doing this, but it'll work for now
        animR = heartR.GetComponent<Animator>();
        animM = heartM.GetComponent<Animator>();
        animL = heartL.GetComponent<Animator>();
	}

	void Update () {



        //Restarts level when player dies
		if (playerHealth.tries <= 0) 
		{
            RestartLevel();
		}

        if (player.transform.position.y <= -10) {
            playerHealth.TakeDamage();
            RestartLevel();
        }

        //Controls the health/hearts UI for the player
        if (playerHealth.tries == 2) {
            animR.SetBool("isHurt", true);
        }
        if (playerHealth.tries == 1) {
            animM.SetBool("isHurt", true);
        }
        if (playerHealth.tries == 0)
        {
            animL.SetBool("isHurt", true);
        }


	}

    void RestartLevel() {
        Debug.Log("Counting down...");
        restartTimer += Time.deltaTime;

        if (restartTimer >= restartDelay)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
