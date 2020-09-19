using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] private float InteractableDistance= .5f;
    private UnityAction NearInteractable;
    private GameObject Player;
    public virtual void Awake()
    {
        Player = Player?? GameObject.FindGameObjectWithTag("Player");// If player is null, assign a player
        NearInteractable += Player.GetComponent<PlayerClass>().NearInteractable;// Adds this to the listener
       
    }
    public virtual void DetectPlayer()
    {
        if ((Player.transform.position - transform.position).magnitude <= InteractableDistance)// If player is within interactable distance
            if (Input.GetButtonDown("Interact"))
                NearInteractable.Invoke();
    }
}
