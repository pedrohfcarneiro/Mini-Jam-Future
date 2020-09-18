using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartGame()// Function designed to change the scene from the Start Menu to the Zone_01 Scene when called
    {
        /////******* ANIMATION IS REQUIRED ******//////
        SceneManager.LoadSceneAsync("Zone_01", LoadSceneMode.Single);// Load this scene and close all other current scenes
    }
    public void ExitGame()// Function designed to close the application upon being called
    {
        /////******* ANIMATION IS REQUIRED ******//////
        Application.Quit();// Closes the application, only works on executable
    }
}
