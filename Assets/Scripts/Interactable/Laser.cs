using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Targetable
{
    [SerializeField] private bool Active;
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
