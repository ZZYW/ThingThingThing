using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zaha : Thing {
    protected override void TTTStart(){
        settings.mass = 0.1f;
        settings.acceleration = 75;
        InvokeRepeating("RandomSetDestination", 5, 5);
        settings.alwaysFacingTarget = true;
        settings.cameraOffset = 30;
    }
    protected override void TTTUpdate() {
    }
    protected override void OnMeetingSomeone(GameObject other) {
        Speak("Aloha" + other.name);
        PlaySound(75);        
        Spark(Color.blue, 100);
    }
    protected override void OnSunset() {
        PlaySound(25);
    }
    protected override void OnSunrise() {
        Spark(Color.red, 100);
    }
}
