using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : Thing {
    protected override void TTTStart(){
        settings.mass = 0.5f;
        settings.acceleration = 1;
        InvokeRepeating("RandomSetDestination", 10, 10);
    }

    protected override void TTTUpdate()
    {
    //here    
    }


    protected override void OnMeetingSomeone(GameObject other)
    {
        Speak ("Stupid!" + other.name);
        PlaySound(5);
        CreateCube();
        Spark(new Color(Random.Range (0,1),1,1,1),29);
    }

    protected override void OnSunset()
    {
        PlaySound(9);
    }

}
