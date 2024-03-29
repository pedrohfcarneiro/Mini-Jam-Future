﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerClass : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    private Rigidbody2D RB2D;// Rigidbody2D type variable that will be used to verify if the player's velocity is different than 0
    private CharacterController2D Controller;// Character controller script type that will be used to determine if the player is grounded
    private ManagerOfScene managerScene;
    private TrackingManager TrackManager;
    private float timer = 0;
    void Start()
    {
        RB2D = GetComponent<Rigidbody2D>();// Reference to the Rigidbody2D component in this object is set
        Controller = GetComponent<CharacterController2D>();// Reference to the Character Controller 2D Script in this object is set
        managerScene = GameObject.FindObjectOfType<ManagerOfScene>();
        TrackManager = GameObject.Find("TrackingManager").GetComponent<TrackingManager>();// Reference is set
    }

    // Update is called once per frame
    void Update()
    {
        if(Controller.m_Grounded)/// Character is grounded
        {
        	animator.SetBool("Grounded", true);
        	animator.SetBool("Falling", false);
        	animator.SetBool("Jumping", false);

            if(Mathf.Abs(RB2D.velocity.x)>0.7)// If the speed's magnitude is higher than 0
            {
            	animator.SetBool("IsWalking", true);/// Character is moving
                animator.SetBool("IsVibing", false);
            }
            else 
            {
                if(animator.GetBool("IsWalking"))
                {
                    timer = 0;
                }
            	animator.SetBool("IsWalking", false);/// Character is not moving
                if (timer >= 1f)
                {
                    animator.SetBool("IsVibing", true);
                }
                else
                    timer += Time.deltaTime;
            }
        }
        else
        {
        	animator.SetBool("Grounded", false);
            animator.SetBool("IsVibing", false);
            if(RB2D.velocity.y<=-0.3)
            {
            	animator.SetBool("Jumping", false);
            	animator.SetBool("Falling", true); // Falling Animation
            }
            else if (RB2D.velocity.y>=0.3)
            {
            	animator.SetBool("Falling", false);
            	animator.SetBool("Jumping", true);// Rising animation
            	
            }
            
        }
    }
    public void NearInteractable()
    {
        //** Do something **//
    }
    public void Interacting()
    {
        //** Do Something **//
        Debug.Log("Interagindo com o objecto");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "StartLevelTrigger")
        {
            if (!managerScene.isRewinding && !managerScene.replay)         //Quando o player começa a gravar enquanto está rolando replay, o clone fica muito rápido
            {
                TrackManager.StartRecording();
            }
            managerScene.OnLevelStart();  
            Debug.Log("chamou onTriggerExit");
        }
    }
}
