using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

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
    public bool isRewinding = false;
    public bool replay = false;
    public float rewindSpeed = 2f;

    #region Initial Positions
    public List<PointInTime> playersInitialPositions = new List<PointInTime>();
    public List<PointInTime> platformsInitialPositions = new List<PointInTime>();
    private Vector3 initialRoomPosition = new Vector3();
    #endregion

    private GameObject playerOriginal;
    private GameObject[] playerClones = new GameObject[4];
    public int numberOfClones = 0;
    private int index = 0;
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
            Debug.Log("chamou reload");
            Reload();
        }
        if(replay)
        {
            Debug.Log("chamou replay");
            Replay();
        }
        if(isRewinding)
        {
            Debug.Log("chamou rewind");
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
        playerOriginal = GameObject.Instantiate(Resources.Load("Prefabs/MC_blue") as GameObject, initialRoomPosition, playersInitialPositions[0].rotation);
        playerOriginal.tag = "Player";
        movementProgress = 0;
        StartRewind();
        reload = false;
    }

    public void Rewind()
    {
        bool closeToNext = false;
        if (index <= TrackingManager.Instance.loop1_Points.Count && index >= 0)
        {
            if ((TrackingManager.Instance.loop1_Points[index].position - playerClones[0].transform.position).magnitude <= 0.06f)
            {
                closeToNext = true;
            }
        }
        if(playerClones != null)
        {
            if (index <= TrackingManager.Instance.loop1_Points.Count && index >= 0)
            {
                playerClones[0].transform.position = TrackingManager.Instance.loop1_Points[index].position;
                playerClones[0].transform.rotation = TrackingManager.Instance.loop1_Points[index].rotation;
                movementProgress += Time.deltaTime * rewindSpeed;
                //Debug.Log(movementProgress);
                if (closeToNext)
                {
                    Debug.Log(TrackingManager.Instance.loop1_Points[index].position);
                    index = index - 2;
                    Debug.Log(index);
                    movementProgress = 0;
                }
            }
            else
            {
                StopRewinding();
            }
        }
    }

    public void Replay()
    {
        bool closeToNext = false;
        if (index < TrackingManager.Instance.loop1_Points.Count && index >= 0)
        {
            if ((TrackingManager.Instance.loop1_Points[index].position - playerClones[0].transform.position).magnitude <= 0.06f)
            {
                closeToNext = true;
            }
        }
        if (playerClones != null)
        {
            if (index < TrackingManager.Instance.loop1_Points.Count && index >= 0)
            {
                playerClones[0].transform.position = TrackingManager.Instance.loop1_Points[index].position;
                playerClones[0].transform.rotation = TrackingManager.Instance.loop1_Points[index].rotation;
                movementProgress += Time.deltaTime * rewindSpeed;
                //Debug.Log(movementProgress);
                if (closeToNext)
                {
                    Debug.Log(TrackingManager.Instance.loop1_Points[index].position);
                    index = index + 2;
                    Debug.Log(index);
                    movementProgress = 0;
                }
            }
            else
            {
                StopReplay();
            }
        }
    }

    public void StartRewind()
    {
        index = TrackingManager.Instance.loop1_Points.Count - 1;
        isRewinding = true;
    }

    public void StopRewinding()
    {
        isRewinding = false;
        StartReplay();
    }

    public void StartReplay()
    {
        index = 1;
        replay = true;
    }
    public void StopReplay()
    {
        replay = false;
    }

    public void OnLevelStart()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PointInTime playerInitialPoint = new PointInTime(player.transform.position, player.transform.rotation);
        playersInitialPositions.Insert(0, playerInitialPoint);
        Debug.Log("Guardou posição inicial");
    }
}
