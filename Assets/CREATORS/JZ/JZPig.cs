using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JZPig : Thing
{

    // Use this for initialization

    protected override void TTTAwake()
    {
        settings.cameraOffset = 30;
        settings.acceleration = 6;
        settings.drag = 1f;
        settings.mass = 0.5f;
        settings.getNewDestinationInterval = 3;
        settings.newDestinationRange = 300;
        settings.myCubeColor = new Color(0.45f, 0.2f, 0.25f);
    }

    protected override void TTTStart()
    {
        Speak("为什么?");
        InvokeRepeating("RandomSetDestination", 3f, 2f);
    }
    protected override void OnMeetingSomeone(GameObject other)
    {
        Speak("Hehe");

        InvokeRepeating("RandomSetDestination", 2f, 4f);
        InvokeRepeating("ResetPosition", 4f, 6f);
    }

    protected override void OnLeavingSomeone(GameObject other)
    {
        Speak("再见!");
        RandomSetDestination();
        InvokeRepeating("RandomSetDestination", 5f, 2f);
        SetScale(new Vector3(15, 15, 15));
    }


    protected override void OnSunset()
    {
        settings.myCubeColor = new Color(0.7f, 0f, 0f);

    }

    // Update is called once per frame

}
