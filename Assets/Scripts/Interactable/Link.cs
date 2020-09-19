using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : Targetable
{
    public override void ExecuteAction()// This object's action when its interactable counterpart is actuated
    {
        transform.parent.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        gameObject.SetActive(false);
    }
}