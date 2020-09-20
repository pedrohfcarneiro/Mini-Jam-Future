using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiLever : Lever
{
    private int whichSprite=0;
    [SerializeField]private Stages[] LeverStages;
    public override void Actuated()
    {
        Interacting.Invoke();// Calls all functions tied to interacting with an interactable object
        if (Flippable)
        {
            if (!Flip)
                GetComponent<SpriteRenderer>().sprite = Sprites[whichSprite+1];// Next Sprite state
            else
                GetComponent<SpriteRenderer>().sprite = Sprites[whichSprite-1];// Previous Sprite state
            if (whichSprite == Sprites.Length || whichSprite == 0)// If it's on either end of the sprites
                Flip = !Flip;
        }
        else if(whichSprite<Sprites.Length)
            GetComponent<SpriteRenderer>().sprite = Sprites[whichSprite+1];// Next stage of actuation
    }
    private class Stages
    {
        private Targetable[] Sets;
    }
}
