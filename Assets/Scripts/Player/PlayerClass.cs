using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerClass : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D RB2D;// Rigidbody2D type variable that will be used to verify if the player's velocity is different than 0
    private CharacterController2D Controller;// Character controller script type that will be used to determine if the player is grounded
    void Start()
    {
        RB2D = GetComponent<Rigidbody2D>();// Reference to the Rigidbody2D component in this object is set
        Controller = GetComponent<CharacterController2D>();// Reference to the Character Controller 2D Script in this object is set
    }

    // Update is called once per frame
    void Update()
    {
        if(Controller.m_Grounded)/// Character is grounded
        {
            if(Mathf.Abs(RB2D.velocity.x)>=0)// If the speed's magnitude is higher than 0
            {
                /// Character is moving
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

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "StartLevelTrigger")
        {
            ManagerOfScene.Instance.OnLevelStart();
            Debug.Log("chamou onTriggerExit");
        }
    }
}
