using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : Targetable
{
    public override void ExecuteAction()// This object's action when its interactable counterpart is actuated
    {
        transform.parent.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
        gameObject.SetActive(false);
    }
}
