using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Targetable
{
    private bool Active = false;
    public override void ExecuteAction()
    {
        GetComponent<Renderer>().enabled= !GetComponent<Renderer>().enabled;// Activates the Renderer
        Active = true;// Now lasers are active
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(Active)
            UnityEditor.EditorApplication.isPlaying = false;// Editor leaves play mode
    }
}
