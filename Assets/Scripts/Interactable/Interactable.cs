using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] private float InteractableDistance= .5f;// Defines the distance from which the player can interact with this
    public UnityAction NearInteractable;
    public UnityAction Interacting;
    public GameObject Player;// Player Reference
    [SerializeField] public Targetable[] Targets;// Target objects reference
    [SerializeField] private bool Reusable = false;// Boolean to determine if this can be interacted with more than once
    private bool Used=false;// Boolean to determine if, if its not reusable, this has been actuated once or not
    private TrackingManager managerTracking;
    private ManagerOfScene managerScene;
    public virtual void Awake()
    {
        if (Player == null)
            Player = GameObject.FindGameObjectWithTag("Player");
        managerTracking = GameObject.FindObjectOfType<TrackingManager>();
        ManagerOfScene.startReplayEvent += PlayerUpdate;
        NearInteractable += Player.GetComponent<PlayerClass>().NearInteractable;// Adds this to the listener
        Interacting += Player.GetComponent<PlayerClass>().Interacting;// Adds this to the Listener
        foreach (Targetable Target in Targets)// Go through the list of Targetable objects
            if (Target != null)// If not null
                Interacting += Target.ExecuteAction;// Stores the reference to the linked object
    }
    private void Start()
    {
        managerScene = GameObject.Find("SceneManager").GetComponent<ManagerOfScene>();
        
    }
    private void Update()
    {
        DetectPlayer();
    }
    public virtual void DetectPlayer()// Function designed to detect if the player is withing interactable distance from the object
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        if(Player!=null)
        if ((Player.transform.position - transform.position).magnitude <= InteractableDistance)// If player is within interactable distance
        {
            NearInteractable.Invoke();// Calls all functions tied to  being near an interactable object
            if (Input.GetButtonDown("Interact") && !Used)// If the player presses the interact button and is able to use it
            {
                managerScene = GameObject.Find("SceneManager").GetComponent<ManagerOfScene>();
                Debug.Log("Actuated");
                Actuated();// Function that will do something when actuated(Animation, specific effects...)
                if (!Reusable)// If it is not reusable
                    Used = true;// Can't use it again
                Debug.Log("Nome do Manager, antes do erro: " + managerScene.name);
                if (managerScene.CheckIfReplayIsDone())
                {
                    Player = GameObject.FindGameObjectWithTag("Player");
                    Player.GetComponent<Tracker>().interactions.Add(Player.GetComponent<Tracker>().index, this.gameObject);
                    Debug.Log(Player.GetComponent<Tracker>().index);
                }
            }
            else if(Input.GetButtonUp("Interact"))// When the user lets go of the button
            {
                OnUnActuation();// Function designed to deal with special cases when releasing the interactive object should do something
            }
        }
    }
    public  virtual void OnUnActuation()
    {
        // Do something//
    }
    public abstract void Actuated();// Function that will do something when actuated(Animation, specific effects...)
    public void PlayerUpdate()
    {
        Debug.Log("Atualizou o player");
        Player = GameObject.FindGameObjectWithTag("Player");
    }
}
