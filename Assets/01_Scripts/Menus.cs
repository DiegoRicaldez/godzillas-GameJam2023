using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void Exit()
    {
        Debug.Log("Saliendo del juego");
        Application.Quit();
    }

    public void Continue()
    {
        Manager.instance.pauseMenu2();
	}

    public void Return()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
