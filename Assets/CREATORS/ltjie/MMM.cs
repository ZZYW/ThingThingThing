using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMM: Thing {
    protected override void TTTStart()
    {
        settings.mass = 0.5f;
        settings.acceleration = 20;
        InvokeRepeating("RandomSetDestination",2,4);
             
     
     }
    protected override void TTTUpdate()
    {
    }
    protected override void OnMeetingSomeone(GameObject other)
    {
        Speak("Hello,MMM!" + other.name );
        PlaySound(4);
        Spark(new Color(Random.Range(0, 1), 30, 1, 1), 28);
    }
    protected override void OnSunset()
    {
        Speak("Good nigth,MMM!");
 
    }

    
}
