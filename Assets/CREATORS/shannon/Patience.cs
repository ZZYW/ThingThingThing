using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patience : Thing {

    protected override void TTTStart(){
        settings.mass = 5;
        settings.acceleration = 70;
        settings.newDestinationRange = 20;

        InvokeRepeating("RandomSetDestination",2, 8);
    }
    protected override void TTTUpdate()
    {
 
    }

    protected override void OnMeetingSomeone(GameObject other)
    {
        Speak("就不让你走！"+other.name);
        PlaySound(96);
    }
    protected override void OnNeighborSpeaking()
    {
        CreateCube();


        

    }
}

