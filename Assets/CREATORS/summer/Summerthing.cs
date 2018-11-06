using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summerthing : Thing {

    protected override void TTTStart() {
        
        settings.mass = 0.2f;
        settings.acceleration = 10;
        settings.newDestinationRange = 40;

        InvokeRepeating("RandomSetDestination", 10, 10);
    }

    protected override void TTTUpdate(){

    }

    protected override void OnMeetingSomeone(GameObject other) {
        StopMoving(2);
        Speak("HELLO THERE" + other.name);
        settings.chatBubbleOffsetHeight = 20;
    }

    protected override void OnLeavingSomeone(GameObject other) {
        PlaySound(Random.Range(1, 102));
        Spark(Color.blue, 30);
    }

}
