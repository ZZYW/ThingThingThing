using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jq : Thing {
    
    protected override void TTTAwake()
    {
        settings.cameraOffset = 15;

        settings.acceleration = 5;
        settings.drag = 5f;
        settings.mass = 0.2f;

        settings.getNewDestinationInterval = 5;
        settings.newDestinationRange = 200;

    }

    protected override void TTTStart()
    {
        Speak("Here I am!");
        InvokeRepeating("RandomSetDestination", 1f, 5f);
    }

    protected override void OnMeetingSomeone(GameObject other)
    {
        Speak("Nice to meet you!");
    }

    protected override void OnLeavingSomeone(GameObject other)
    {
        PlaySound(2);

    }

    protected override void OnSunrise()
    {
        StopMoving(10);
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
