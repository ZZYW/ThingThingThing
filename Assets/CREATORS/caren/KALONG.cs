using System.Collections;
using System.Collections.Generic;
using UnityEngine;






public class KALONG : Thing
{
     AudioSource FCsound;
   protected override void TTTStart()
    {
        settings.drag = 0.4f;
        settings.mass = 0.3f;
      settings.acceleration = 4;
        Spark(new Color(Random.Range(0, 1), 50, 10, 14), 29);
        FCsound = GetComponent<AudioSource>();
        FCsound.Play(0);
        transform.Translate(new Vector3(0, 0, 1));

     // Invok   eRepeating("SetRandomTarget", 10, 10);
    }

    protected override void OnSunrise()
    {
        StopMoving(10);
        ChangeColor(Color.yellow);
    }

    protected override void OnSunset()
    {

        ChangeColor(Color.cyan);
    }


    protected override void OnNeigborSparkingParticles()
    {
        Speak("Oooops");
    }

    protected override void OnMeetingSomeone(GameObject other)
    {
        Speak("LALALADUDUDUDADABANG!");
        CreateCube();
    }
    protected override void TTTUpdate()
    {
        //
    }

}