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
    public void QuitGame()
    {
        /////////********* ANIMATION REQUIRED ****************//////////////////
        Application.Quit();// Closes the application, only works on executable
    }
}
