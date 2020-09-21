using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ManagerOfScene : MonoBehaviour
{
    
    
    public bool reload = false;
    public bool isRewinding = false;
    public bool replay = false;
    public float rewindSpeed = 2f;

    #region Initial Positions
    public List<PointInTime> playersInitialPositions = new List<PointInTime>();
    public List<PointInTime> platformsInitialPositions = new List<PointInTime>();
    private Vector3 initialRoomPosition = new Vector3();
    #endregion

    public GameObject playerOriginal;
    public GameObject[] playerClones = new GameObject[4];
    private int index = 0;
    private float movementProgress = 0;

    #region SceneObjects
    public int numberOfClones = 0;
    public List<GameObject> droppingBoxes = new List<GameObject>();
    public List<Tracker> trackersInScene = new List<Tracker>();
    #endregion

    public delegate void Management();
    public static event Management rewindEvent;
    public static event Management replayEvent;
    public static event Management startRewindEvent;
    public static event Management startReplayEvent;

    private void Awake()
    {
        
    }
    private void OnDestroy()
    {
        rewindEvent = null;
        replayEvent = null;
        startRewindEvent = null;
        startReplayEvent = null;
}
    void Start()
    {
        playerOriginal = GameObject.FindGameObjectWithTag("Player");
        initialRoomPosition = GameObject.FindGameObjectWithTag("InitialRoom").transform.position;

        //Find Trackers in Scene
        Tracker[] obs = FindObjectsOfType<Tracker>();
        foreach(Tracker tr in obs)
        {
            trackersInScene.Add(tr);
        }
    }

    void Update()
    {
        if(reload)
        {
            //Debug.Log("chamou reload");
            Reload();
        }
        if(replay)
        {
            //Debug.Log("chamou replay");
            Replay();
        }
        if(isRewinding)
        {
            //Debug.Log("chamou rewind");
            
            Rewind();
        }
        
    }

    public void Reload()
    {
        if(numberOfClones == 0)
        {
            playerClones[0] = playerOriginal;
            playerClones[0].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            playerClones[0].GetComponent<Rigidbody2D>().simulated = false;
            playerClones[0].GetComponent<Movement>().enabled = false;
            playerClones[0].GetComponent<CharacterController2D>().enabled = false;
            playerClones[0].tag = "Untagged";
            numberOfClones++;
        }
        reload = false;
        StartRewind();
    }

    public void Rewind()
    {
        rewindEvent();

        if(CheckIfRewindIsDone())
        {
            StopRewinding();
        }
    }

    public void Replay()
    {
        replayEvent();

        if (CheckIfReplayIsDone())
        {
            StopReplay();
        }
    }

    public void StartRewind()
    {
        startRewindEvent();
        isRewinding = true;
    }

    public void StartReplay()
    {
        startReplayEvent();
        replay = true;
    }

    public void StopRewinding()
    {
        isRewinding = false;
        playerOriginal = GameObject.Instantiate(Resources.Load("Prefabs/MC_blue") as GameObject, initialRoomPosition, Quaternion.identity);
        playerOriginal.GetComponent<Tracker>().canRewind = false;
        playerOriginal.GetComponent<Tracker>().canReplay = false;
        playerOriginal.tag = "Player";
        playerOriginal.GetComponent<Tracker>().canRewind = true;
        foreach (GameObject droppingBox in droppingBoxes)
        {
            Debug.Log("Acabou o rewind?: " + droppingBox.GetComponent<Tracker>().rewindDone);
            if (droppingBox.GetComponent<Tracker>().rewindDone)
            {
                Debug.Log("Tentando congelar a caixa");
                droppingBox.GetComponent<DroppingBox>().myChain.Freeze();
            }
        }
        StartReplay();
    }
    public void StopReplay()
    {
        replay = false;
        playerOriginal.GetComponent<Tracker>().canReplay = true;
    }

    
    

    public bool CheckIfRewindIsDone()
    {
        int counter = 0;
        foreach(Tracker tr in trackersInScene)
        {
            if (tr.rewindDone)
                counter++;
        }
        if (counter == trackersInScene.Count)
            return true;
        else
            return false;
    }

    public bool CheckIfReplayIsDone()
    {
        int counter = 0;
        foreach (Tracker tr in trackersInScene)
        {
            if (tr.replayDone)
                counter++;
        }
        if (counter == trackersInScene.Count)
            return true;
        else
            return false;

    }

    public void OnLevelStart()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PointInTime playerInitialPoint = new PointInTime(player.transform.position, player.transform.rotation);
        playersInitialPositions.Insert(0, playerInitialPoint);
        Debug.Log("Guardou posição inicial");
    }
}
