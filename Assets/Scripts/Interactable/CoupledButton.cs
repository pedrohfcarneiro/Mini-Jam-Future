using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoupledButton : Interactable
{
    [SerializeField]private bool OtherPressed=false;// Boolean to indicate if the other coupled button is being pressed
    [SerializeField] private CoupledButton OtherButton;// Reference to the Couple Button should be set on inspector
    public override void Awake()
    {
        Interacting += OtherButton.setPressed;// Pressing the button is being passed to the Unity Action
        if (Player == null)
            Player = GameObject.FindGameObjectWithTag("Player");
        NearInteractable += Player.GetComponent<PlayerClass>().NearInteractable;// Adds this to the listener
        Interacting += Player.GetComponent<PlayerClass>().Interacting;// Adds this to the Listener
    }
    public override void Actuated()// Called when the player interacts with it
    {
        if(OtherPressed)// Verify if the other button is being pressed at the moment this one is being pressed
        {  
            foreach (Targetable Target in Targets)// Go through the list of Targetable objects
                if (Target != null)// If not null
                    Interacting += Target.ExecuteAction;// Stores the reference to the linked object
        }
        Interacting.Invoke();// Invoke all assigned functions
    }
    public override void OnUnActuation()// When this button is unpressed
    {
        base.OnUnActuation();
        foreach (Targetable Target in Targets)// Go through the list of Targetable objects
            if (Target != null)// If not null
                Interacting -= Target.ExecuteAction;// Stores the reference to the linked object
        if (ManagerOfScene.Instance.CheckIfReplayIsDone())
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            Player.GetComponent<Tracker>().interactions.Add(Player.GetComponent<Tracker>().index, this.gameObject);
            Debug.Log(Player.GetComponent<Tracker>().index);
        }
        Interacting.Invoke();
    }
    private void setPressed()
    {
        
        this.OtherPressed = !OtherPressed;// Update the current state of the other button
        Debug.Log("Estado atual do botão:" + OtherPressed);
    }
}
