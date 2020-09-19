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
    public bool moveClones = false;
    public float cloneSpeed = 2f;

    #region Initial Positions
    public List<PointInTime> playersInitialPositions = new List<PointInTime>();
    public List<PointInTime> platformsInitialPositions = new List<PointInTime>();
    private Vector3 initialRoomPosition = new Vector3();
    #endregion

    private GameObject playerOriginal;
    private GameObject[] playerClones = new GameObject[4];
    public int numberOfClones = 0;
    private float movementProgress = 0;

    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        playerOriginal = GameObject.FindGameObjectWithTag("Player");
        initialRoomPosition = GameObject.FindGameObjectWithTag("InitialRoom").transform.position;
    }

    void Update()
    {
        if(reload)
        {
            Reload();
        }
        if(moveClones)
        {
            MoveClones();
        }
    }

    public void Reload()
    {
        if(numberOfClones == 0)
        {
            playerClones[0] = playerOriginal;
            playerClones[0].GetComponent<Movement>().enabled = false;
            playerClones[0].tag = "Untagged";
            numberOfClones++;
        }
        playerOriginal = GameObject.Instantiate(Resources.Load("Prefabs/Player") as GameObject, initialRoomPosition, playersInitialPositions[0].rotation);
        playerOriginal.tag = "Player";
        moveClones = true;
        movementProgress = 0;
        reload = false;
    }

    public void MoveClones()
    {
        bool closeToNext = false;
        if (TrackingManager.Instance.loop1_Points.Count > 0)
        {
            if ((TrackingManager.Instance.loop1_Points[0].position - playerClones[0].transform.position).magnitude <= 0.2f)
                closeToNext = true;
        }
        if(playerClones != null)
        {
            if (TrackingManager.Instance.loop1_Points.Count > 0)
            {
                playerClones[0].transform.position = Vector3.Lerp(playerClones[0].transform.position, TrackingManager.Instance.loop1_Points[0].position, movementProgress);
                playerClones[0].transform.rotation = TrackingManager.Instance.loop1_Points[0].rotation;
                movementProgress += Time.deltaTime * cloneSpeed;
                Debug.Log(movementProgress);
                if (closeToNext)
                {
                    TrackingManager.Instance.loop1_Points.RemoveAt(0);
                    movementProgress = 0;
                }
            }
            else
            {
                StopCloneMovement();
            }
        }
    }

    public void StopCloneMovement()
    {
        moveClones = false;
    }

    public void OnLevelStart()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PointInTime playerInitialPoint = new PointInTime(player.transform.position, player.transform.rotation);
        playersInitialPositions.Insert(0, playerInitialPoint);
        Debug.Log("Guardou posição inicial");
    }
}
