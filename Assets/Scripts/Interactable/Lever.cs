using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Interactable
{
    // Update is called once per frame
    [SerializeField]private Sprite[] Sprites = new Sprite[2];// The two sprite states
    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = Sprites[0];// Initial state of the sprite
    }
    public override void Actuated()
    {
        GetComponent<SpriteRenderer>().sprite = Sprites[1];// Initial state of the sprite
    }
}
