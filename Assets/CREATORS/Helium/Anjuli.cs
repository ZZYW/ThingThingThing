using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anjuli : Thing {

    protected override void TTTStart(){
    	settings.mass = 0.5f;
    	settings.acceleration = 6;
        settings.chatBubbleOffsetHeight = 100;
        InvokeRepeating("RandomSetDestination", 10, 10);
    }

    protected override void TTTUpdate(){

    }

    protected override void OnMeetingSomeone(GameObject other){
    	Speak("d=====(￣▽￣*)b" + other.name);
        Spark(new Color(4,43,2,5),100);
    }
}
