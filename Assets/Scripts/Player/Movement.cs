using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 Direction;// 2D Vector
    private CharacterController Controller;// Holds a CharacterController type component
    [SerializeField] private float Speed = 1f;// Float number that controls the speed of movement(Linear Proportion)
    [SerializeField] private float GravityForce = -10f;// Float number that controls the force of Gravity
    [SerializeField] private float JumpForce = 30f;// How fast/high the jump can go
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
    private float YForce;// Variable of type float that holds a velocity on the Y axis
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    void Start()
    {
        Controller = GetComponent<CharacterController>();// Reference to the component in the player object is set
    }

    // Update is called once per frame
    void Update()
    {
       
        CharacterMove();// Moves the character every frame based on the Horizontal
        VerticalMove();// Moves the characyer downward
    }
    private void CharacterMove()
    {
        
        Direction = new Vector2(Input.GetAxis("Horizontal"), 0);// Creates a 2D Vector that points on the X axis
        Controller.Move(Direction * Time.deltaTime * Speed);// Moves the player object
    }
    private void VerticalMove()
    {
        bool isGrounded=false;
        Collider[] colliders = Physics.OverlapSphere(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                isGrounded = true;
        }
        if (isGrounded)// If the controller is touching ground
        {
            YForce = 0;// Reset YForce
            if (Input.GetButtonDown("Jump"))// If the player presses the jump button
                YForce += JumpForce;// Add a high value for jump
        }
        YForce += GravityForce * Time.deltaTime;// Increases Y velocity downwards every frame
        Direction = new Vector2(0, YForce);// Creates a 2D Vector that points on the Y axis
        Controller.Move(Direction * Time.deltaTime);// Moves the player object
    }
}
