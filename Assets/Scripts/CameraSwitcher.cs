using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{

    public static System.Action OnCameraSwitch;
    public static CameraSwitcher main;

    public Transform ActiveCam { get; private set; }

    [Range(1, 200)]
    public float intervals = 20f;
    public bool switching = true;

    [SerializeField] GameObject mainCam;
    [SerializeField] GameObject followCam;

    List<GameObject> allThings;
    bool useMain = false;
    CameraFollowThing cameraFollowThing;

    int switchCounter = 0;
    int mainCamTurnQueue = 5;
    float hue = 0;
    float time = 0;

    void Awake()
    {
        main = this;
    }

    // Use this for initialization
    void Start()
    {
        cameraFollowThing = followCam.GetComponent<CameraFollowThing>();

        allThings = ThingManager.main.AllThings;

        if (allThings.Count < 1) return;
        AssignFollowTarget();

        InvokeRepeating("ChangeCamera", intervals, intervals);
        SetActiveCameras();
    }

    // Update is called once per frame
    void Update()
    {
        if (allThings.Count < 1) return;
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            ChangeCamera();
        }

        //update background color
        Color bgColor = Color.HSVToRGB(hue, 0.3f, 0.73f);
        Camera.main.backgroundColor = bgColor;
        time += 0.1f * Time.deltaTime;
        hue = (Mathf.Sin(time) + 1) / 2f;
    }

    void AssignFollowTarget()
    {
        GameObject oneRandomThing = allThings[(int)Random.Range(0, allThings.Count)];
        cameraFollowThing.followTarget = oneRandomThing.transform;

        // float desiredFollowDistance = 1;
        // desiredFollowDistance = oneRandomThing.GetComponent<Thing>().DesiredFollowDistance;
        cameraFollowThing.followDistance = 0.4f;
    }

    void ChangeCamera()
    {
        switchCounter++;

        if (switchCounter % mainCamTurnQueue == 0)
        {
            useMain = true;
        }
        else
        {
            useMain = false;
        }


        //randomly choose one Thing
        if (!useMain) AssignFollowTarget();
        if (!switching) return;

        SetActiveCameras();
    }

    void SetActiveCameras()
    {
        mainCam.SetActive(useMain);
        followCam.SetActive(!useMain);
        ActiveCam = useMain ? mainCam.transform : followCam.transform;
        if (OnCameraSwitch != null) OnCameraSwitch();
    }

}