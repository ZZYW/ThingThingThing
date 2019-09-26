using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YW : Thing
{
    protected override void ThingAwake()
    {
        settings.cameraOffset = 15;
        settings.acceleration = 4;
        settings.drag = 1.8f;
        settings.mass = 100f;
        settings.chatBubbleOffsetHeight = 10;
        settings.getNewDestinationInterval = 5;
        settings.newDestinationRange = 40;
        settings.alwaysFacingTarget = true;
        settings.neighborDetectorRadius = 10;
    }

    protected override void ThingStart()
    {
        Speak("hey yo!");
        PlaySound();

    }

    protected override void ThingUpdate()
    {

    }

    protected override void OnSunset()
    {
        Spark(Color.blue, 100);
        Speak("sad day..");
      

    }

    protected override void OnSunrise()
    {
        Speak("Hey!");
    }

    protected override void OnMeetingSomeone(GameObject other) { }
    protected override void OnLeavingSomeone(GameObject other) { }
    protected override void OnNeighborSpeaking() { }
    protected override void OnNeigborSparkingParticles() { }

}
