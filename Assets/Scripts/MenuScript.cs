using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject controlUI = null;
    public GameObject creditsUI = null;
    public GameObject mainUI = null;
    public int gameScene = 1;


    public void EnableMain()
    {
        controlUI.SetActive(false);
        creditsUI.SetActive(false);
        mainUI.SetActive(true);
    }


    public void EnableControls()
    {
        controlUI.SetActive(true);
        mainUI.SetActive(false);
    }


    public void EnableCredits()
    {
        creditsUI.SetActive(true);
        mainUI.SetActive(false);
    }


    public void PlayGame()
    {
        SceneManager.LoadScene(gameScene);
    }


    public void ExitGame()
    {
        Application.Quit();
    }
}
