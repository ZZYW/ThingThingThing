using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minh : Thing {
	protected override void TTTAwake()
	{
		cameraOffset = 10;

		acceleration = 10;
		drag = 5f;
		mass = 0.2f;

		getNewDestinationInterval = 5;
		newDestinationRange = 100;
	}

	protected override void TTTStart()
	{
		Speak ("Hello World!!!");
		InvokeRepeating ("RandomSetDestination", 1f, 5f);
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
		StopMoving();
	}

	protected override void OnNeighborSpeaking()
	{
		Spark(Color.yellow, 100);
	}

}
