using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadScene("4Players");
    }

    public void PlayGame3()
    {
        SceneManager.LoadScene("3Players");
    }

    public void PlayGame2()
    {
        SceneManager.LoadScene("2Players");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
