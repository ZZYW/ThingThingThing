using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nole : Thing {
    protected override void TTTStart () {
        settings.mass = 0.5f;
        settings.acceleration = 1;

        InvokeRepeating ("RandomSetDestination", 10, 10);

    }
    protected override void TTTUpdate () {

    }
    protected override void OnMeetingSomeone (GameObject other) {
        Speak ("@#$!#$#%#$^%#^#$" + other.name);
    }
}