using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Interactable
{
    // Update is called once per frame
    [SerializeField]private Sprite[] Sprites = new Sprite[2];// The two sprite states
    private bool Flip=false;
    [SerializeField] private bool Flippable=false;
    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = Sprites[0];// Initial state of the sprite
    }
    public override void Actuated()
    {
        if(Flippable)
        {
            if (!Flip)
                GetComponent<SpriteRenderer>().sprite = Sprites[1];// Actuated state of the sprite
            else
                GetComponent<SpriteRenderer>().sprite = Sprites[0];// Initial state of the sprite
            Flip = !Flip;// Inverts which state it is
        }
        else
            GetComponent<SpriteRenderer>().sprite = Sprites[1];// Actuated state of the sprite

    }
}
