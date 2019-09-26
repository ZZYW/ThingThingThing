using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hana : Thing {
    protected override void TTTStart()
    {
        settings.mass = 10;
      
        GetComponent<Rigidbody>().mass = 10;
        settings.acceleration = 1;
        settings.newDestinationRange = 50;
        //settings.alwaysFacingTarget = true;

        InvokeRepeating("RandomSetDestination",1,1);
    }

    protected override void TTTUpdate()
    {
        //每隔60s左右更新一次
    }

    protected override void OnMeetingSomeone(GameObject other)
    {
        Speak("跟我一起去玩吧!!!" + other.name);
        PlaySound(27);
        //Spark(Color.green,10);
        Spark(new Color(Random.Range(0,1),1,1,1),  10);
    }

    protected override void OnSunset()
    {
        PlaySound(88);
    }

    protected override void OnSunrise()
    {
        PlaySound(34);
        CreateCube();
    }

}
