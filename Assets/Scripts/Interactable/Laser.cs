using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Targetable
{
    private bool Active;
    private void Start()
    {
        Active = GetComponent<Renderer>().enabled;// Active starts the same as the renderer
    }
    public override void ExecuteAction()
    {
        GetComponent<Renderer>().enabled= !GetComponent<Renderer>().enabled;// Activates the Renderer
        Active = !Active;// Invert if players are 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(Active)
            UnityEditor.EditorApplication.isPlaying = false;// Editor leaves play mode
    }
}
