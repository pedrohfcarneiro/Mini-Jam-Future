using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : Targetable
{
    [SerializeField]private Rigidbody2D[] ObjectsToHold;// Rigidbody2D that will be used to hold the objects in place and release them
    [SerializeField] private Sprite[] Sprites = new Sprite[2];// The two sprite states
    private void Start()
    {
        Freeze();
        GetComponent<SpriteRenderer>().sprite = Sprites[0];// Change the Sprite to the one that shows it not actuated
    }
    public override void ExecuteAction()// This object's action when its interactable counterpart is actuated
    {
        foreach (Rigidbody2D Object in ObjectsToHold)
            Object.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;// Objects can move
        GetComponent<SpriteRenderer>().sprite = Sprites[1];// Change the Sprite to the one that shows it has actuated
    }

    public void Freeze()
    {
        Debug.Log("Congelou a caixa");
        foreach (Rigidbody2D Object in ObjectsToHold)
            Object.constraints = RigidbodyConstraints2D.FreezeAll;// Objects can't move
    }
}