using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingTomas : Thing {


	//once
	protected override void TTTAwake ()
	{
		// how far the following camera will be from my object
		cameraOffset = 15;

		acceleration = 4f;
		drag = 0f; //the bigger the drag is, the slower your thing moves
		mass = 0.2f; 


		getNewDestinationInterval = 5; //in seconds
		newDestinationRange = 40;

		myCubeColor = new Color (0, 0, 1); // red green blue 0-1
	}

	//once
	protected override void TTTStart () 
	{
		//base.TTTStart();
		// everything inside this code block will only be exe once

		Speak ("¡Aló my friend!");

		InvokeRepeating("RandomSetDestination", 0f,3f);
	}

	protected override void TTTUpdate()
	{
		//base.TTUpdate();
		// everything inside this code block will be exe many many times
		// about ~ 60 times per second
	}
		
	// below are all events

	protected override void OnMeetingSomeone ( GameObject other)
	{
		//base.OnMeetingSomeone(other);
		PlaySound("glitchedtones_Robot Chatter 03");

		//everything inside this code block will be triggered/called
		//when my thing meets anyone
	}

	protected override void OnLeavingSomeone ( GameObject other)
	{
		//base.OnLeavingSomeone(other);
	}

	protected override void OnSunset ()
	{
		//this code will be triggered when the sun is setting
		//base.OnSunset();
	}

	protected override void OnSunrise()
	{
		Spark (Color.white, 60);
		Speak("so bright!");
	}

	protected override void OnTouchWater()
	{
		//base.OnTouchWater();
		ChangeColor (Color.yellow);
	}

	protected override void OnNeighborSpeaking()
	{
		//base.OnNeighborSpeaking();
		for (int i = 0; 1 < 5; i++) {
			CreateCube ();
		}
	}

	protected override void OnNeigborSparkingParticles()
	{
		//base.OnNeighborSparkingParticles();
	}

}
