using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{

    public GameObject mainCam;
    public GameObject followCam;


    ThirdPersonCamera.CameraController camController;

    GameObject[] allThings;

    bool useMain = true;

    [Range(1, 60)]
    public float intervals = 20f;

    // Use this for initialization
    void Start()
    {
        camController = followCam.GetComponent<ThirdPersonCamera.CameraController>();


        allThings = GameObject.FindGameObjectsWithTag("Thing");

        AssignFollowTarget();

        InvokeRepeating("ChangeCamera", intervals, intervals);
        SetActiveCameras();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.A))
        {
            ChangeCamera();
        }
    }

    void AssignFollowTarget()
    {
        GameObject oneRandomThing = allThings[(int)Random.Range(0, allThings.Length)];
        camController.target = oneRandomThing.transform;

        float desiredFollowDistance = 1;
        desiredFollowDistance =  oneRandomThing.GetComponent<Creature>().desiredFollowDistance;
        camController.desiredDistance = desiredFollowDistance;
    }


    void ChangeCamera()
    {
        //randomly choose one Thing
        if (!useMain) AssignFollowTarget();
   
        //toggle
        useMain = !useMain;
        SetActiveCameras();
    }

    void SetActiveCameras()
    {
        mainCam.SetActive(useMain);
        followCam.SetActive(!useMain);
    }


}
