using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallZ : Thing
{

    protected override void TTTStart()
    {
        settings.mass = 0.5f;
        settings.acceleration = 5;
        settings.chatBubbleOffsetHeight = 2;
        settings.cameraOffset = 15;
        InvokeRepeating("RandomSetDestination", 10, 10);
    }
    protected override void TTTUpdate()
    {
    }




    protected override void OnMeetingSomeone(GameObject other)
    {
        Speak("Biubiubiubiubiu!" + other.name);
        PlaySound(15);

    }

    protected override void OnSunrise()
    {
        CreateCube();
    }
}



