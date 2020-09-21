using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePlatform : Interactable
{
    private TrackingManager TrackManager;
    private ManagerOfScene ManagerScene;
    public override void Awake()
    {
        base.Awake();
        TrackManager = GameObject.Find("TrackingManager").GetComponent<TrackingManager>();// Reference is set
        ManagerScene = GameObject.Find("SceneManager").GetComponent<ManagerOfScene>();// Reference is set
    }
    public override void Actuated()
    {
        TrackManager.StopRecording();
        ManagerScene.reload = true;
    }
}
