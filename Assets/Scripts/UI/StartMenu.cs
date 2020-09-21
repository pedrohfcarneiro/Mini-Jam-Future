using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        /////////********* ANIMATION REQUIRED ****************//////////////////
        SceneManager.LoadSceneAsync("Zone_01", LoadSceneMode.Single);// Loads the new scene and erases all currently loaded scene
    }
    public void StartCredits()
    {
        /////////********* ANIMATION REQUIRED ****************//////////////////
        SceneManager.LoadSceneAsync("Credits", LoadSceneMode.Single);// Loads the new scene and erases all currently loaded scene
    }
    public void ReturnMenu()
    {
        /////////********* ANIMATION REQUIRED ****************//////////////////
        SceneManager.LoadSceneAsync("Start Menu", LoadSceneMode.Single);// Loads the new scene and erases all currently loaded scene
    }
    public void QuitGame()
    {
        /////////********* ANIMATION REQUIRED ****************//////////////////
        Application.Quit();// Closes the application, only works on executable
    }
}
