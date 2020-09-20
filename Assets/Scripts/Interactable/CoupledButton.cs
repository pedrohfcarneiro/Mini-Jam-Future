using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoupledButton : Interactable
{
    [SerializeField]private bool OtherPressed=false;// Boolean to indicate if the other coupled button is being pressed
    [SerializeField] private CoupledButton OtherButton;// Reference to the Couple Button should be set on inspector
    public override void Actuated()// Called when the player interacts with it
    {
        Debug.Log("Pressed the button");
        if (OtherPressed)// If it is being pressed
            Interacting.Invoke();// Calls all functions tied to interacting with an interactable object
        else
            OtherButton.setPressed(true);// Changes the state on the other button to know this one is being pressed
    }
    public override void OnUnActuation()// When this button is unpressed
    {
        base.OnUnActuation();
        OtherButton.setPressed(false);// Update the state of this  button to the couple button
    }
    private void setPressed(bool newState)
    {
        this.OtherPressed = newState;// Update the current state of the other button
    }
}
