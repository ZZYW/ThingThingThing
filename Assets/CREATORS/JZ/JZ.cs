using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JZ : Thing
{
    protected override void TTTAwake()
    //program will run before start. Only once.
    {

        settings.cameraOffset = 15;
        //you have a big object, the distance needs to be longer.
        //how far the following camera will be from my object.
        settings.acceleration = 4f;
        //the larger number, the faster it accelerates.
        settings.drag = 0.1f; //the bigger the drag is, the slower you thing moves.
        settings.mass = 0.2f;

        settings.getNewDestinationInterval = 5; //in seconds 
        settings.newDestinationRange = 100;

        settings.myCubeColor = new Color(1f, 0f, 0.25f);//red green blue 0-1

    }


    protected override void TTTStart()
    {
        //everything here will only be exe once at the begining 

        Speak("Hmmmm");
        InvokeRepeating("RandomSetDestination", 2f, 8f);
    }


    protected override void TTTUpdate()
    {

        //everything here will be exe many many many times. 
        //About 6 times per sec.
    }


    //below are all events
    protected override void OnMeetingSomeone(GameObject other)
    {

        //base.OnMeetingSomeone(other);
        //everything inside this code block will be triggered/called
        //when my thing meet anyone
    }

    protected override void OnLeavingSomeone(GameObject other)
    {

        //base. OnLeavingSomeone(other)
    }

    protected override void OnSunset()
    {


        //this code will be triggered when the sun is setting 
        //base. OnSunset ()
    }




    protected override void OnNeighborSpeaking()
    {
        CreateCube();

    }


}