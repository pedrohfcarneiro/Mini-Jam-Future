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
            isRecording = false;
        }

        if (isRecording)
            Record();
    }

    public void Record()
    {
        if(canRecord)
        {
            pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));   //Grava uma nova posição e rotação do player
            StopRecording();
        }
    }

    public void StopRecording()                 //Para a gravação
    {
        canRecord = false;
        timer = 0;
    }
}
