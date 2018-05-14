using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class richard : Thing {

	protected override void TTTAwake()
	{
		cameraOffset = 10;

		acceleration = 10;
		drag = 5f;
		mass = 0.2f;

		getNewDestinationInterval = 5;
		newDestinationRange = 100;

		myCubeColor = new Color (0.8f, 0.2f, 0.1f);
	
		
	}

	protected override void TTTStart()
	{
		Speak ("I am alive!!");
		InvokeRepeating ("RandomSetDestination", 1f, 5f);
	}


	protected override void OnMeetingSomeone(GameObject other)
	{
		Speak ("Salut! I am Richard, the Cloud. $%#kfjladkjf&8420");
		CreateCube ();
	}

	protected override void OnLeavingSomeone(GameObject other)
	{
		PlaySound ("zapsplat_multimedia_game_blip_generic_tone_007_17643");
	}

		
	protected override void OnNeigborSparkingParticles()
	{
		Spark(Color.white, 50);
	}


}
