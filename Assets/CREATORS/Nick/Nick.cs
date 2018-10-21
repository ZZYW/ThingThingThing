using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nick : Thing {

    protected override void TTTAwake () {
        settings.cameraOffset = 10;

        settings.acceleration = 10;
        settings.drag = 5f;
        settings.mass = 0.2f;

        settings.getNewDestinationInterval = 5;
        settings.newDestinationRange = 200;

        //myCubeColor = new Color (0.8f, 0.2f, 0.1f);
    }

    protected override void TTTStart () {
        Speak ("Heckaboutit");
        InvokeRepeating ("RandomSetDestination", 1f, 5f);
    }

    protected override void OnMeetingSomeone (GameObject other) {
        Speak ("Hey - I'm Nick");
        CreateCube ();
    }

    protected override void OnLeavingSomeone (GameObject other) {
        PlaySound ("glitchedtones_Robot Chatter 01");
        //Speak ("hello" + other.name);
        //CreateCube ();
    }

    protected override void OnTouchWater () {
        RandomSetDestination (); //get a new random target
    }

    protected override void OnSunset () {
        //StopMoving(3.0f);
    }
}