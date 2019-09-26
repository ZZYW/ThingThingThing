using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCP2046 : Thing{
    // Use this for initialization

    protected override void TTTStart() {
        settings.mass = 0.02f;
        settings.acceleration = 10;
        InvokeRepeating("RandomSetDestination", 3, 3);
    }

    protected override void TTTUpdate() {
        //here
    }

    protected override void OnMeetingSomeone(GameObject other) {  
        Speak ("233333333333" + other.name);
        PlaySound(2);
        Spark(Color.cyan, 20); }
        

    protected override void OnLeavingSomeone(GameObject other)
    {
        //Example:
        Spark(Color.red, 15);
    }

    protected override void OnSunset() {
        PlaySound(84);
    }
}