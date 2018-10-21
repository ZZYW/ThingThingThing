using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minh : Thing {
	protected override void TTTAwake()
	{
		settings.cameraOffset = 10;

		settings.acceleration = 10;
		settings.drag = 5f;
		settings.mass = 0.2f;

		settings.getNewDestinationInterval = 5;
		settings.newDestinationRange = 100;
	}

	protected override void TTTStart()
	{
		Speak ("Hello World!!!");
		InvokeRepeating ("RandomSetDestination", 2f, 5f);
	}

	protected override void OnMeetingSomeone(GameObject other)
	{
		Speak ("Hello! I am beautiful cactus");
	}

	protected override void OnLeavingSomeone(GameObject other)
	{
        PlaySound ("zapsplat_animals_bird_cockatiel_single_chirp_003_17575");
	}

	protected override void OnSunrise()
	{
		//StopMoving();
        Spark(Color.yellow, 4);
	}

	protected override void OnNeighborSpeaking()
	{
		
	}

}
