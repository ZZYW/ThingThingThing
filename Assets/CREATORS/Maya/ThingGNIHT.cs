using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingGNIHT : Thing
{
    protected override void TTTAwake()
    {
        settings.acceleration = Random.Range(1f, 3f);
        settings.drag = 0.4f;
        settings.mass = 2.0f;
        settings.getNewDestinationInterval = 5;
        settings.newDestinationRange = 50;
        settings.myCubeColor = Color.white;
    }


    protected override void TTTStart()
    {
        InvokeRepeating("RandomSetDestination", 0, settings.getNewDestinationInterval);
    }


    protected override void OnMeetingSomeone(GameObject other)
    {
        StopMoving(2.0f);
    }

    protected override void OnLeavingSomeone(GameObject other)
    {
        RestartWalking();
    }

    protected override void OnTouchWater()
    {
        Vector3 newScale = new Vector3(3.0f, 3.0f, 3.0f);
        SetScale(newScale);
    }

    protected override void OnLeaveWater()
    {
        Vector3 newScale = new Vector3(-3.0f, -3.0f, -3.0f);
        SetScale(newScale);
    }

    protected override void OnNeigborSparkingParticles()
    {
        if (NeighborCount > 1)
        {
            CreateCube();
        }
    }
}