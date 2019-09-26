using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep_mushy : Thing {

	//once
	protected override void TTTAwake()
	{
		// how far the following camera will be from my object
		settings.cameraOffset = 15;

		settings.acceleration = 60f;
		settings.drag = 1f; // the bigger the drag is, the slower your thing moves
		settings.mass = 0.1f;

		settings.getNewDestinationInterval = 5; // in seconds
		settings.newDestinationRange = 100;

		settings.myCubeColor = new Color (1, 0, 0); //RGB range is 0-1
	}

	//once
	protected override void TTTStart()
	{
		//base.TTTstart();
		// everything inside this code block will only be exe once

		Speak ("I am right here!");

		InvokeRepeating ("RandomSetDestination", 0f, 10f);

	}

	protected override void TTTUpdate()
	{
		//base.TTTUpdate();
		// everything inside this code block will be exe many many times
		// about ~ 60 times per second
	}


	// below are all events

	protected override void OnMeetingSomeone(GameObject other)
	{
		//base.OnMeetingSomeone(other);

		PlaySound (44);

		//everything inside this code block will be triggered/called
		//when my thing meets anyone
	}

	protected override void OnLeavingSomeone(GameObject other)
	{
		//base.OnLeavingSomeone(other);
		Speak ("Bye bye!");
	}

	protected override void OnSunset()
	{
		//this code will be triggered when the sun is setting
		//base.OnSunset()
		ChangeColor(Color.white);
	}

	protected override void OnSunrise()
	{
		Spark (Color.red, 50);
		Speak ("Alo Alo");
	}

	protected override void OnTouchWater()
	{
		//base.OnTouchWater();
		ChangeColor(Color.red);
		StopMoving ();
	}

	protected override void OnLeaveWater()
	{
		//base.OnNeighborSpeaking();
		Spark (Color.blue, 30);
	}

	protected override void OnNeighborSpeaking()
	{
		//base.OnNeighborSpeaking();
		CreateCube();

	}

	protected override void OnNeigborSparkingParticles()
	{
		//base.OnNeighborSparkingParticles();
		Speak ("That looks awesome!");
	}
}
