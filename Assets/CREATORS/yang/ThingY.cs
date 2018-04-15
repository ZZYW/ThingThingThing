using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingY : Thing
{


    //once
    protected override void TTTAwake()
    {
        // how far the following camera will be from my object
        cameraOffset = 15;

        acceleration = 4f;
        drag = 1.0f; //the bigger the drag is, the slower your thing moves
        mass = 0.2f;

        getNewDestinationInterval = 5; //in seconds
        newDestinationRange = 40;

        myCubeColor = new Color(1, 0.2f, 0); // red green  blue  0-1
    }

    //once
    protected override void TTTStart()
    {
        //base.TTTStart();
        // everything inside this code block will only be exe once

        Speak("I am born!!!!!!!!!!!!!!!");

        InvokeRepeating("RandomSetDestination", 0f, 10f);

    }

    protected override void TTTUpdate()
    {
        //base.TTTUpdate();
        // evertying inside this code block will be exe many many times
        // about ~ 60 times per second
    }

    // below are all events
    protected override void OnMeetingSomeone(GameObject other)
    {
        //base.OnMeetingSomeone(other);

        PlaySound("zapsplat_animals_bird_cockatiel_single_chirp_003_17575");

        //evething inside this code block will be triggered/called
        //when my thing meets anyone
    }

    protected override void OnLeavingSomeone(GameObject other)
    {
        //base.OnLeavingSomeone(other);
    }

    protected override void OnSunset()
    {
        // this code will be triggered when the sun is setting
        //base.OnSunset();
    }

    protected override void OnSunrise()
    {
        Spark(Color.yellow, 50);
        Speak("i love the sun");
    }

    protected override void OnTouchWater()
    {
        //base.OnTouchWater();
        ChangeColor(Color.red);
    }

    protected override void OnLeaveWater()
    {
        //base.OnLeaveWater();
        ResetColor();
    }

    protected override void OnNeighborSpeaking()
    {
        //base.OnNeighborSpeaking();
        for (int i = 0; i < 5; i++)
        {
            CreateCube();
        }

    }
    protected override void OnNeigborSparkingParticles()
    {
        //base.OnNeigborSparkingParticles();
    }









}
