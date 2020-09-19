using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] private float InteractableDistance= .5f;// Defines the distance from which the player can interact with this
    private UnityAction NearInteractable, Interacting;
    private GameObject Player;// Player Reference
    public virtual void Awake()
    {
        Player = Player?? GameObject.FindGameObjectWithTag("Player");// If player is null, assign a player
        NearInteractable += Player.GetComponent<PlayerClass>().NearInteractable;// Adds this to the listener
        Interacting += Player.GetComponent<PlayerClass>().Interacting;// Adds this to the Listener
    }
    public virtual void DetectPlayer()// Function designed to detect if the player is withing interactable distance from the object
    {
        if ((Player.transform.position - transform.position).magnitude <= InteractableDistance)// If player is within interactable distance
        {
            NearInteractable.Invoke();// Calls all functions tied to  being near an interactable object
            if (Input.GetButtonDown("Interact"))// If the player presses the interact button
                Interacting.Invoke();// Calls all functions tied to interacting with an interactable object
        }
    }
    
}
