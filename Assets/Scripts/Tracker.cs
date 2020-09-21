using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    [SerializeField] private float timeBetweenRecordings;
    [SerializeField] private float timeOfSingleRewind;
    [SerializeField] private float timeOfSingleReplay;
    private ManagerOfScene managerScene;
    private TrackingManager managerTracking;
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
        managerScene = GameObject.FindObjectOfType<ManagerOfScene>();
        managerTracking = GameObject.FindObjectOfType<TrackingManager>();
        pointsInTime = new List<PointInTime>(); 
        timer = 0;
        index = 0;
        canRecordSingle = true;
        replayDone = true;
        managerTracking.isRecording = false;
        #endregion

        //Add me to manager
        switch (this.tag)
        {
            case "DroppingBox":
                managerScene.droppingBoxes.Add(this.gameObject);
                break;
            case "Clone":
                managerScene.trackersInScene.Add(this);
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
            managerTracking.StartRecording();
        }
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            managerTracking.StopRecording();
            managerScene.reload = true;
        }

        if (managerTracking.isRecording)
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
        if (gameObject != null)
        {
            if (this.tag == "Player")
            {
                switch (managerTracking.numberOfCurrentLoops)
                {
                    case 0:
                        managerTracking.numberOfCurrentLoops++;
                        managerTracking.loopsPoints.Insert(0, new List<PointInTime>(this.pointsInTime));     //Envia informação do player1 para o TrackingManager (loop1_Points)
                        whichIAm = 0;
                        break;
                    case 1:
                        managerTracking.numberOfCurrentLoops++;
                        managerTracking.loopsPoints.Insert(1, new List<PointInTime>(this.pointsInTime));
                        whichIAm = 1;
                        break;
                }
            }
            else if (this.tag == "DroppingBox")
            {
                switch (managerTracking.DroppingBoxesPoints.Count)
                {
                    case 0:
                        managerTracking.DroppingBoxesPoints.Insert(0, new List<PointInTime>(this.pointsInTime));     //Envia informação do player1 para o TrackingManager (loop1_Points)
                        whichIAm = 0;
                        break;
                    case 1:
                        managerTracking.DroppingBoxesPoints.Insert(1, new List<PointInTime>(this.pointsInTime));
                        whichIAm = 1;
                        break;
                    case 2:
                        managerTracking.DroppingBoxesPoints.Insert(2, new List<PointInTime>(this.pointsInTime));     //Envia informação do player1 para o TrackingManager (loop1_Points)
                        whichIAm = 2;
                        break;
                    case 3:
                        managerTracking.DroppingBoxesPoints.Insert(3, new List<PointInTime>(this.pointsInTime));     //Envia informação do player1 para o TrackingManager (loop1_Points)
                        whichIAm = 3;
                        break;
                    case 4:
                        managerTracking.DroppingBoxesPoints.Insert(4, new List<PointInTime>(this.pointsInTime));     //Envia informação do player1 para o TrackingManager (loop1_Points)
                        whichIAm = 4;
                        break;
                    default:
                        break;
                }
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
            foreach (KeyValuePair<int, GameObject> pair in interactions)
            {
                if (pair.Key == index && !interactingWithKeyValue)
                {
                    Debug.Log("interagiu dnv");
                    pair.Value.GetComponent<Interactable>().Interacting.Invoke();
                    interactingWithKeyValue = true;
                }
            }
            if (index <= managerTracking.loopsPoints[0].Count && index >= 0)
            {
                if ((managerTracking.loopsPoints[0][index].position - transform.position).magnitude <= 0.06f)
                {
                    closeToNext = true;
                }
            }
            if (managerScene.playerClones != null)
            {
                if (index <= managerTracking.loopsPoints[0].Count && index >= 0)
                {
                    transform.position = managerTracking.loopsPoints[0][index].position;
                    transform.rotation = managerTracking.loopsPoints[0][index].rotation;
                    if (closeToNext)
                    {
                        Debug.Log(managerTracking.loopsPoints[0][index].position);
                        if (rewindTimer >= timeOfSingleRewind)
                        {
                            interactingWithKeyValue = false;
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
            if (index <= managerTracking.DroppingBoxesPoints[whichIAm].Count && index >= 0)
            {
                if ((managerTracking.DroppingBoxesPoints[whichIAm][index].position - transform.position).magnitude <= 0.06f)
                {
                    closeToNext = true;
                }
            }
            if (managerScene.droppingBoxes != null)
            {
                if (index <= managerTracking.DroppingBoxesPoints[whichIAm].Count && index >= 0)
                {
                    transform.position = managerTracking.DroppingBoxesPoints[whichIAm][index].position;
                    transform.rotation = managerTracking.DroppingBoxesPoints[whichIAm][index].rotation;
                    if (closeToNext)
                    {
                        Debug.Log(managerTracking.DroppingBoxesPoints[whichIAm][index].position);
                        if (rewindTimer >= timeOfSingleRewind)
                        {
  
                            index = index - 1;
                            rewindTimer = 0;
                        }
                        Debug.Log(index);
                    }
                }
                else if((managerTracking.DroppingBoxesPoints[whichIAm][0].position-transform.position).magnitude<=.5f)
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
            }
            if (index < managerTracking.loopsPoints[0].Count && index >= 0)
            {
                if ((managerTracking.loopsPoints[0][index].position - transform.position).magnitude <= 0.06f)
                {
                    closeToNext = true;
                }
            }
            if (managerScene.playerClones != null)
            {
                if (index < managerTracking.loopsPoints[0].Count && index >= 0)
                {
                    transform.position = managerTracking.loopsPoints[0][index].position;
                    transform.rotation = managerTracking.loopsPoints[0][index].rotation;
                    if (closeToNext)
                    {
                        if (replayTimer >= timeOfSingleReplay)
                        {
                            interactingWithKeyValue = false;
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
