using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingDummy : Thing {

    protected override void TTTAwake()
    {
        //camera
        cameraOffset = 15; //distance from camera to object center on 3rd personn camera following mode

        //Movement
        acceleration = Random.Range(3.5f, 5f); //use Random.range to get a random number within a range
        drag = 1.8f; // the bigger, the slower
        mass = 0.2f; // the bigger, the heavier, the more acceleration it needs to get this moving, also can push away lighter THINGS
        getNewDestinationInterval = 5; //how often to get a new target to run to, in (seconds)
        newDestinationRange = 40; // how far the new destination could be 
    }

    //only run once at the start
    protected override void TTTStart()
    {
        //we recommend keep this one, or you can write your own rule of moving
        InvokeRepeating("RandomSetDestination", 0, getNewDestinationInterval); //call "RandomSetDestination" method every "getNewDestinationInterval" seconds
    }

    //run about 40~60 times per second, infinitely
    protected override void TTTUpdate()
    {

    }

    protected override void OnMeetingSomeone(GameObject other)
    {

    }

    protected override void OnLeavingSomeone(GameObject other)
    {

    }

    protected override void OnNeighborSpeaking()
    {

    }

    protected override void OnTouchWater()
    {

    }

    protected override void OnLeaveWater()
    {

    }

    protected override void OnNeigborSparkingParticles()
    {

    }

    protected override void OnSunset()
    {

    }

    protected override void OnSunrise()
    {

    }

}
