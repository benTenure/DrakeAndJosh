using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

	public void PlayGame()
    {
        SceneManager.LoadScene("Level00");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
