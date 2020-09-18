using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 Direction;
    private CharacterController Controller;
    void Start()
    {
        Controller = GetComponent<CharacterController>();// Reference to the component in the player object
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void CharacterMove()
    {

    }
}
