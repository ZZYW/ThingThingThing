using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jq : Thing {
    protected override void TTTAwake()
    {
        cameraOffset = 15;

        acceleration = 5;
        drag = 5f;
        mass = 0.2f;

        getNewDestinationInterval = 5;
        newDestinationRange = 200;

    }

    protected override void TTTStart()
    {
        Speak("Here I am!");
        InvokeRepeating("RandomSetDestination", 1f, 5f);
    }

    protected override void OnMeetingSomeone(GameObject other)
    {
        Speak("Nice to meet you!");
        CreateCube();
    }

    protected override void OnLeavingSomeone(GameObject other)
    {
        PlaySound("cartoon-pinch");
        CreateCube();
    }

    protected override void OnSunrise()
    {
        StopMoving();
        ChangeColor(Color.yellow);
    }

    protected override void OnSunset()
    {
        ChangeColor(Color.cyan);
    }


    protected override void OnNeigborSparkingParticles()
    {
        Speak("Oooops");
    }
}
