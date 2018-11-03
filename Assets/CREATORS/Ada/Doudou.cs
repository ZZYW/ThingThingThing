using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doudou : Thing {

    protected override void TTTStart(){
        settings.mass = 0.2f;
        settings.acceleration = 2;
        settings.newDestinationRange = 200;
        InvokeRepeating("RandomSetDestination", 2, 3);

    }

    protected override void TTTUpdate()
    {

    }

    protected override void OnMeetingSomeone(GameObject other)
    {
        Speak("(= =)~~~~"+other.name);
    }

}
