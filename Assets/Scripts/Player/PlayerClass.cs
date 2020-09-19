using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerClass : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
