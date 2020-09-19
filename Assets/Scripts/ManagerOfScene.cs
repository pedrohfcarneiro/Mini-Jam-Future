using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerOfScene : MonoBehaviour
{
    #region Singleton
    private static ManagerOfScene _instance;

    public static ManagerOfScene Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ManagerOfScene>();
                if (_instance == null)
                {
                    _instance = new GameObject().AddComponent<ManagerOfScene>();
                }
            }
            return _instance;
        }
    }

    #endregion

    public bool reload = false;

    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(reload)
        {
            Reload();
        }
    }

    public void Reload()
    {

    }

    public void OnLevelStart()
    {

    }
}
