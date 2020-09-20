using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : Interactable
{
    public override void Actuated()// When the player interacts with this object
    {
        Debug.Log("Loading new scene");
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);// Load next scene and closes all currently active scenes
    }
}
