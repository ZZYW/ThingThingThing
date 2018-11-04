using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shapeofsinking :Thing {

    protected override void TTTStart()
    {
        settings.mass = 0.1f;
        settings.acceleration = 6;
        settings.chatBubbleOffsetHeight = 1200;


        InvokeRepeating("RandomSetDestination", 8, 8);
       
    
    }


    protected override void TTTUpdate()
    {

    }
    protected override void OnMeetingSomeone(GameObject other)
    {
        Speak ("Hi," + other.name + "I am Yuruojie.");
        PlaySound(15);
        Spark(Color.white,20);


    }
   
}


