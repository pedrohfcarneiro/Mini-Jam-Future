using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    [SerializeField] private float timeBetweenRecordings;
    [SerializeField] private float timeOfSingleRewind;
    [SerializeField] private float timeOfSingleReplay;
    private float timer;
    private float rewindTimer;
    private float replayTimer;
    public int index = 0;
    public bool canRecordSingle = false;
    public bool rewindDone = false;
    public bool replayDone = true;
    public bool canRewind = true;
    public bool canReplay = true;
    public int whichIAm = 0;
    private bool interactingWithKeyValue = false;

    public List<PointInTime> pointsInTime;
    public Dictionary<int, GameObject> interactions = new Dictionary<int, GameObject>();



    private void OnEnable()
    {
        TrackingManager.bringInfo += SendInfo;
        if (this.gameObject.tag == "Player")
        {
            Debug.Log("tapora");
            ManagerOfScene.rewindEvent += MyRewindPlayer;
            ManagerOfScene.replayEvent += MyReplayPlayer;
        }
        else if(this.gameObject.tag == "DroppingBox")
        {
            Debug.Log("eitacaray");
            ManagerOfScene.rewindEvent += MyRewindBox;
        }
        ManagerOfScene.startRewindEvent += MyStartRewind;
        ManagerOfScene.startReplayEvent += MyStartReplay;
    }

    void Start()
    {
        #region Initializations
        pointsInTime = new List<PointInTime>(); 
        timer = 0;
        index = 0;
        canRecordSingle = true;
        replayDone = true;
        TrackingManager.Instance.isRecording = false;
        #endregion

        //Add me to manager
        switch (this.tag)
        {
            case "DroppingBox":
                ManagerOfScene.Instance.droppingBoxes.Add(this.gameObject);
                break;
            case "Clone":
                ManagerOfScene.Instance.trackersInScene.Add(this);
                break;
        }
    }

    void Update()
    {
        //Debug.Log(replayDone);
        if(!canRecordSingle)
        {
            timer += Time.deltaTime;
            if(timer >= timeBetweenRecordings)  //Limita a gravação ao tempo timeBetweenRecordings
            {
                canRecordSingle = true;               
            }
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            TrackingManager.Instance.StartRecording();
        }
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            TrackingManager.Instance.StopRecording();
            ManagerOfScene.Instance.reload = true;
        }

        if (TrackingManager.Instance.isRecording)
            Record();
    }

    public void Record()
    {
        if(canRecordSingle)
        {
            pointsInTime.Add(new PointInTime(transform.position, transform.rotation));   //Grava uma nova posição e rotação do player
            //Debug.Log(pointsInTime[pointsInTime.Count-1].position);
            StopSingleRecord();
            index++;
        }
    }

    public void StopSingleRecord()                 //Para a gravação
    {
        canRecordSingle = false;
        timer = 0;
    }

    public void SendInfo()
    {
        if (this.tag == "Player")
        {
            switch (TrackingManager.Instance.numberOfCurrentLoops)
            {
                case 0:
                    TrackingManager.Instance.numberOfCurrentLoops++;
                    TrackingManager.Instance.loopsPoints.Insert(0, new List<PointInTime>(this.pointsInTime));     //Envia informação do player1 para o TrackingManager (loop1_Points)
                    whichIAm = 0;
                    break;
                case 1:
                    TrackingManager.Instance.numberOfCurrentLoops++;
                    TrackingManager.Instance.loopsPoints.Insert(1, new List<PointInTime>(this.pointsInTime));
                    whichIAm = 1;
                    break;
            }
        }
        else if(this.tag == "DroppingBox")
        {
            switch (TrackingManager.Instance.DroppingBoxesPoints.Count)
            {
                case 0:
                    TrackingManager.Instance.DroppingBoxesPoints.Insert(0, new List<PointInTime>(this.pointsInTime));     //Envia informação do player1 para o TrackingManager (loop1_Points)
                    whichIAm = 0;
                    break;
                case 1:
                    TrackingManager.Instance.DroppingBoxesPoints.Insert(1, new List<PointInTime>(this.pointsInTime));
                    whichIAm = 1;
                    break;
            }
        }
    }

    public void MyRewindPlayer()
    {
        if (canRewind)
        {
            bool closeToNext = false;
            rewindDone = false;
            rewindTimer += Time.deltaTime;
            if (index <= TrackingManager.Instance.loopsPoints[0].Count && index >= 0)
            {
                if ((TrackingManager.Instance.loopsPoints[0][index].position - transform.position).magnitude <= 0.06f)
                {
                    closeToNext = true;
                }
            }
            if (ManagerOfScene.Instance.playerClones != null)
            {
                if (index <= TrackingManager.Instance.loopsPoints[0].Count && index >= 0)
                {
                    transform.position = TrackingManager.Instance.loopsPoints[0][index].position;
                    transform.rotation = TrackingManager.Instance.loopsPoints[0][index].rotation;
                    if (closeToNext)
                    {
                        Debug.Log(TrackingManager.Instance.loopsPoints[0][index].position);
                        if (rewindTimer >= timeOfSingleRewind)
                        {
                            index = index - 1;
                            rewindTimer = 0;
                        }
                        Debug.Log(index);

                    }
                }
                else
                {
                    rewindDone = true;
                }
            }
        }
    }

    public void MyRewindBox()
    {
        if (canRewind)
        {
            bool closeToNext = false;
            rewindDone = false;
            rewindTimer += Time.deltaTime;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().simulated = false;
            if (index <= TrackingManager.Instance.DroppingBoxesPoints[whichIAm].Count && index >= 0)
            {
                if ((TrackingManager.Instance.DroppingBoxesPoints[whichIAm][index].position - transform.position).magnitude <= 0.06f)
                {
                    closeToNext = true;
                }
            }
            if (ManagerOfScene.Instance.droppingBoxes != null)
            {
                if (index <= TrackingManager.Instance.DroppingBoxesPoints[whichIAm].Count && index >= 0)
                {
                    transform.position = TrackingManager.Instance.DroppingBoxesPoints[whichIAm][index].position;
                    transform.rotation = TrackingManager.Instance.DroppingBoxesPoints[whichIAm][index].rotation;
                    if (closeToNext)
                    {
                        Debug.Log(TrackingManager.Instance.DroppingBoxesPoints[whichIAm][index].position);
                        if (rewindTimer >= timeOfSingleRewind)
                        {
                            index = index - 1;
                            rewindTimer = 0;
                        }
                        Debug.Log(index);
                    }
                }
                else
                {
                    rewindDone = true;
                    GetComponent<Rigidbody2D>().simulated = true;
                }
            }
        }
    }


    public void MyReplayPlayer()
    {
        if (canReplay)
        {
            bool closeToNext = false;
            replayTimer += Time.deltaTime;
            foreach(KeyValuePair<int,GameObject> pair in interactions)
            {
                if(pair.Key == index && !interactingWithKeyValue)
                {
                    Debug.Log("interagiu dnv");
                    pair.Value.GetComponent<Interactable>().Interacting.Invoke();
                    interactingWithKeyValue = true;
                }
                else if(pair.Key != index)
                {
                    interactingWithKeyValue = false;
                }
            }
            if (index < TrackingManager.Instance.loopsPoints[0].Count && index >= 0)
            {
                if ((TrackingManager.Instance.loopsPoints[0][index].position - transform.position).magnitude <= 0.06f)
                {
                    closeToNext = true;
                }
            }
            if (ManagerOfScene.Instance.playerClones != null)
            {
                if (index < TrackingManager.Instance.loopsPoints[0].Count && index >= 0)
                {
                    transform.position = TrackingManager.Instance.loopsPoints[0][index].position;
                    transform.rotation = TrackingManager.Instance.loopsPoints[0][index].rotation;
                    if (closeToNext)
                    {
                        if (replayTimer >= timeOfSingleReplay)
                        {
                            index = index + 1;
                            replayTimer = 0;
                        }
                        Debug.Log(index);
                    }
                }
                else
                {
                    replayDone = true;
                }
            }
        }
    }

    public void MyStartRewind()
    {
        if (pointsInTime != null && canRewind)
        {
            index = pointsInTime.Count - 1;
            rewindTimer = 0;
        }
    }

    public void MyStartReplay()
    {
        replayDone = false;
        if (pointsInTime != null && canReplay)
        {
            index = 0;
            replayTimer = 0;
        }
    }

    
}
