using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : Thing {

    protected override void TTTStart() {
        settings.mass = 999999999999;
        settings.acceleration = 3;
        InvokeRepeating("RandomSetDestination",2,2);
        
    }
    protected override void TTTUpdate()
    {
       
    }

    protected override void OnMeetingSomeone(GameObject other)
    {
        Speak("Yummy!PIW!うまい!" + other.name);
        PlaySound(75);
        CreateCube();
        Spark(Color.grey, 50);
    }

    protected override void OnSunset()
    {
        PlaySound(60);
    }

}
