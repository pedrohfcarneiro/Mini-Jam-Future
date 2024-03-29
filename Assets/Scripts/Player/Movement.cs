﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController2D Controller;// Holds a CharacterController type component
    [SerializeField] private float Speed = 10f;// Float number that controls the speed of movement(Linear Proportion)
    private bool Jump=false;
    void Start()
    {
        Controller = GetComponent<CharacterController2D>();// Reference to the component in the player object is set
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))// If the player presses the jump button
            Jump = true;
    }
    private void FixedUpdate()
    {
        CharacterMove();// Moves the character every frame
    }
    private void CharacterMove()
    {
        
        Controller.Move(Speed * Input.GetAxis("Horizontal"), false,Jump);// Moves the player object
        Jump = false;
    }
}
