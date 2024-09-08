using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    
    public GameObject gameOverUI;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void gameOver()
    {
        gameOverUI.SetActive(true);
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void startMenu()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void testMenu()
    {
        SceneManager.LoadScene("TestScene");
    }

    public void creditsMenu()
    {
        SceneManager.LoadScene("CreditsScene 1");
    }


    public void quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
