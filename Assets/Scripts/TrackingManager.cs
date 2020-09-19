using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingManager : MonoBehaviour
{
    #region Singleton
    private static TrackingManager _instance;

    public static TrackingManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<TrackingManager>();
                if(_instance == null)
                {
                    _instance = new GameObject().AddComponent<TrackingManager>();
                }
            }
            return _instance;
        }
    }

    #endregion

    public int numberOfCurrentLoops = 0;
    public List<PointInTime> loop1_Points = new List<PointInTime>();
    public List<PointInTime> loop2_Points = new List<PointInTime>();
    
    
    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
