using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud_a : Thing {

	protected override void TTTAwake()
	{
		settings.cameraOffset = 15;

		settings.acceleration = 50;
		settings.drag = 5f;
		settings.mass = 0.2f;

		settings.getNewDestinationInterval = 5;
		settings.newDestinationRange = 200;

		settings.myCubeColor = new Color (0.8f, 0.2f, 0.1f);
	}

	protected override void TTTStart()
	{
		Speak ("FLOATING...");
		InvokeRepeating ("RandomSetDestination", 1f, 5f);
	}

	protected override void OnMeetingSomeone(GameObject other)
	{
		Speak ("Salut!");
		CreateCube ();
	}

	protected override void OnLeavingSomeone(GameObject other)
	{
		PlaySound ("glitchedtones_Robot Chatter 03");
		CreateCube ();
	}

	protected override void OnSunrise()
	{
		ChangeColor (Color.yellow);
	}

	protected override void OnSunset()
	{
		ChangeColor (Color.cyan);
	}


	protected override void OnNeigborSparkingParticles()
	{
		Speak ("Oooops");
	}



}
