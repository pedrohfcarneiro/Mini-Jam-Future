using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingManager : MonoBehaviour
{
    public int numberOfCurrentLoops = 0;
    public List<List<PointInTime>> loopsPoints = new List<List<PointInTime>>();
    public List<List<PointInTime>> DroppingBoxesPoints = new List<List<PointInTime>>();
    public bool isRecording = false;
    public delegate void BringInfoFromStuff();
    public static event BringInfoFromStuff bringInfo;


    private void Awake()
    {
        
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
    private void OnDestroy()
    {
        bringInfo = null;
    }
}
