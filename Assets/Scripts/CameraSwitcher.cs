using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour {

    public GameObject mainCam;
    public GameObject followCam;
    public bool switching = true;
    CameraFollowThing cameraFollowThing;

    public List<GameObject> allThings;

    bool useMain = true;

    [Range (1, 200)]
    public float intervals = 20f;

    // Use this for initialization
    void Start () {
        cameraFollowThing = followCam.GetComponent<CameraFollowThing> ();

        allThings = ThingManager.main.AllThings;

        if (allThings.Count < 1) return;
        AssignFollowTarget ();

        InvokeRepeating ("ChangeCamera", intervals, intervals);
        SetActiveCameras ();
    }

    // Update is called once per frame
    void Update () {
        if (allThings.Count < 1) return;
        if (Input.GetKeyUp (KeyCode.A)) {
            ChangeCamera ();
        }
    }

    void AssignFollowTarget () {
        GameObject oneRandomThing = allThings[(int) Random.Range (0, allThings.Count)];
        cameraFollowThing.followTarget = oneRandomThing.transform;

        float desiredFollowDistance = 1;
        desiredFollowDistance = oneRandomThing.GetComponent<Thing> ().DesiredFollowDistance;
        cameraFollowThing.followDistance = desiredFollowDistance;
    }

    void ChangeCamera () {

        if (!switching) return;
        //randomly choose one Thing
        if (!useMain) AssignFollowTarget ();

        //toggle
        useMain = !useMain;
        SetActiveCameras ();
    }

    void SetActiveCameras () {
        mainCam.SetActive (useMain);
        followCam.SetActive (!useMain);
    }

}