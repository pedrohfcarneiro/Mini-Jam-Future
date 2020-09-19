using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    public bool isRecording;
    private bool canRecord;
    [SerializeField]private float timeBetweenRecordings;
    private float timer;

    public List<PointInTime> pointsInTime;
    
    void Start()
    {
        #region Initializations
        pointsInTime = new List<PointInTime>(); 
        timer = 0;
        canRecord = true;
        isRecording = false;
        #endregion

    }

    void Update()
    {
        if(!canRecord)
        {
            timer += Time.deltaTime;
            if(timer >= timeBetweenRecordings)  //Limita a gravação ao tempo timeBetweenRecordings
            {
                canRecord = true;               
            }
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            isRecording = true;
        }
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            StopRecording();
            ManagerOfScene.Instance.reload = true;
        }

        if (isRecording)
            Record();
    }

    public void Record()
    {
        if(canRecord)
        {
            pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));   //Grava uma nova posição e rotação do player
            //Debug.Log(pointsInTime[pointsInTime.Count-1].position);
            StopSingleRecord();
        }
    }

    public void StopSingleRecord()                 //Para a gravação
    {
        canRecord = false;
        timer = 0;
    }

    public void StopRecording()
    {
        isRecording = false;
        switch(TrackingManager.Instance.numberOfCurrentLoops)
        {
            case 0:
                TrackingManager.Instance.numberOfCurrentLoops++;
                TrackingManager.Instance.loop1_Points = this.pointsInTime;
                Debug.Log(TrackingManager.Instance.loop1_Points[0].position);
                break;
            case 1:
                TrackingManager.Instance.numberOfCurrentLoops++;
                TrackingManager.Instance.loop2_Points = this.pointsInTime;
                Debug.Log(TrackingManager.Instance.loop1_Points[0].position);
                break;
        }
    }
}
