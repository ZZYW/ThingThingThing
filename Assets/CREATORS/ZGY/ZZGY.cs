using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZZGY : Thing
{

    protected override void TTTStart()
    {
        settings.mass = 300f;
        settings.acceleration = 2;
        InvokeRepeating("RandomSetDestination", 50, 3);
    }

    protected override void TTTUpdate()
    {//here

    }
    protected override void OnMeetingSomeone(GameObject other)
    {
        Speak("Hey!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!" + other.name);
        PlaySound(97);
        CreateCube();
        Spark(new Color(Random.Range(0, 1),255,1, 1), 29);
    }
}