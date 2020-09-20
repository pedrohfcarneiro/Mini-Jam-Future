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
    public List<List<PointInTime>> loopsPoints = new List<List<PointInTime>>();
    public List<List<PointInTime>> DroppingBoxesPoints = new List<List<PointInTime>>();
    public bool isRecording = false;
    public delegate void BringInfoFromStuff();
    public static event BringInfoFromStuff bringInfo;


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

    public void StopRecording()
    {
        isRecording = false;
        if(bringInfo != null)
        {
            bringInfo();
        }
    }
    public void StartRecording()
    {
        isRecording = true;
    }
}
