using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : Thing {
    protected override void TTTAwake()
    {
        //camera
        settings.cameraOffset = 30; //distance from camera to object center on 3rd personn camera following mode

        //Movement
        //settings.acceleration = Random.Range(2.5f, 3f); //use Random.range to get a random number within a range
        settings.drag = 1.8f; // the bigger, the slower
        settings.mass = 9.0f; // the bigger, the heavier, the more acceleration it needs to get this moving, also can push away lighter THINGS
        settings.acceleration = 10.0f;
        //settings.getNewDestinationInterval = 5; //how often to get a new target to run to, in (seconds)
        settings.newDestinationRange = 200; // how far the new destination could be 
        settings.chatBubbleOffsetHeight = 12;

        //color
        settings.myCubeColor = Color.red; //cube's color produced by you
    }

    protected override void TTTStart()
    {   
        InvokeRepeating("RandomSetDestination", 0.5f, 0.5f);   
        //InvokeRepeating("RandomSetTarget", 1f, 1f);    
    }

    /*protected override void TTTUpdate()
    {  
        //Speak("###################");
    }*/

    private float max = 20;
    private float min = 10;
    private float scale = 15;

    protected override void OnMeetingSomeone(GameObject other)
    {
        Speak("侬好 #" + other.name.GetHashCode());
        //float s = Random.Range(10, 20);
        if(scale == min)
        {
            scale = max;
        }
        else
        {
            scale = min;
        }
        SetScale(new Vector3(scale, scale, scale));
    }

    protected override void OnLeavingSomeone(GameObject other)
    {
        PlaySound(75);
        Spark(Color.white, 100);
        SetTarget(new Vector3(0f,0f,0f));
        //ResetPosition();
    }
}
