using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingPrinceZ : Thing
{
    protected override void TTTAwake()
    {
        //camera
        settings.cameraOffset = 15; //distance from camera to object center on 3rd personn camera following mode

        //Movement
        settings.acceleration = Random.Range(3.5f, 5f); //use Random.range to get a random number within a range
        settings.drag = 1.8f; // the bigger, the slower
        settings.mass = 0.2f; // the bigger, the heavier, the more acceleration it needs to get this moving, also can push away lighter THINGS
        settings.getNewDestinationInterval = 5; //how often to get a new target to run to, in (seconds)
        settings.newDestinationRange = 40; // how far the new destination could be 

        settings.myCubeColor = Color.green; //cube's color produced by you
    }


    private void ChangeAlpha()
    {
        float tempAlpha = Mathf.Sin(Time.time) + 1;
        Color tempColor = new Color(0.2F, 0.3F, 0.4F, tempAlpha);
        GameObject body = transform.Find("default").gameObject;
        if (body != null)
        {
            //todo: material with transparency
           // body.GetComponent<Renderer>().material.color = tempColor;
        }
    }

    protected override void TTTStart()
    {
        //examples:
        Speak("I am born!");
        //we recommend keep this one, or you can write your own rule of moving
        InvokeRepeating("RandomSetDestination", 0, settings.getNewDestinationInterval); //call "RandomSetDestination" method every "getNewDestinationInterval" seconds
    }

    protected override void TTTUpdate()
    {
        // a bunch of examples:

        if (Input.GetKeyDown(KeyCode.J))
        {

        }

        if (TOD_Data.main.IsNight)
        {

        }

        if (TOD_Data.main.IsDay)
        {
            ChangeAlpha();
        }

        if (InWater)
        {
            
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SetRandomTarget(settings.newDestinationRange);
        }
    }


    protected override void OnMeetingSomeone(GameObject other)
    {
        //Example
        Speak("I met " + other.name, 2f);
    }

    protected override void OnLeavingSomeone(GameObject other)
    {
        //Example:
        Spark(Color.red, 15);
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
        //Example:
        PlaySound(22);
    }


}
