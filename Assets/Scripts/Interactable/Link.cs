using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : Targetable
{
    [SerializeField]private Rigidbody2D[] ObjectsToHold;
    private void Start()
    {
        foreach (Rigidbody2D Object in ObjectsToHold)
            Object.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    public override void ExecuteAction()// This object's action when its interactable counterpart is actuated
    {
        foreach (Rigidbody2D Object in ObjectsToHold)
            Object.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        gameObject.SetActive(false);
    }
}