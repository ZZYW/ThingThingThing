using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingY : Thing
{

    Vector3 originalScale;

    //float sinIndex = 0;

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
        InvokeRepeating("UpUp", 0, 3f);

        originalScale = transform.localScale;
    }

    protected override void TTTUpdate()
    {
        //base.TTTUpdate();
        // evertying inside this code block will be exe many many times
        // about ~ 60 times per second



    }

    void UpUp()
    {
        float upforwardForceMag = 50;
        AddForce(Vector3.up * upforwardForceMag);
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
        for (int i = 0; i < 2; i++)
        {
            CreateCube();
        }

        transform.localScale = originalScale * 2;
    }

    protected override void OnSunrise()
    {
        transform.localScale = originalScale;

        Speak("aaaaa");
    }

    protected override void OnTouchWater()
    {
        ChangeColor(Color.red);
        PlaySound("zapsplat_multimedia_game_blip_generic_tone_007_17643");
    }

    protected override void OnLeaveWater()
    {
        //base.OnLeaveWater();
        ResetColor();
    }

    protected override void OnNeighborSpeaking()
    {
        CreateCube();
    }
    protected override void OnNeigborSparkingParticles()
    {
        CreateCube();
    }









}
