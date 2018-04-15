using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep_mushy : Thing {

	//once
	protected override void TTTAwake()
	{
		// how far the following camera will be from my object
		cameraOffset = 15;

		acceleration = 70f;
		drag = 3f; // the bigger the drag is, the slower your thing moves
		mass = 0.2f;

		getNewDestinationInterval = 5; // in seconds
		newDestinationRange = 100;

		myCubeColor = new Color (1, 0, 0); //RGB range is 0-1
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

		PlaySound ("zapsplat_multimedia_game_ascend_climb_jump_synthesized_17873");

		//everything inside this code block will be triggered/called
		//when my thing meets anyone
	}

	protected override void OnLeavingSomeone(GameObject other)
	{
		//base.OnLeavingSomeone(other);
	}

	protected override void OnSunset()
	{
		//this code will be triggered when the sun is setting
		//base.OnSunset()
	}

	protected override void OnSunrise()
	{
		Spark (Color.yellow, 50);
		Speak ("Alo Alo");
	}

	protected override void OnTouchWater()
	{
		//base.OnTouchWater();
		ChangeColor(Color.red);
	}

	protected override void OnLeaveWater()
	{
		//base.OnNeighborSpeaking();
	}

	protected override void OnNeighborSpeaking()
	{
		//base.OnNeighborSpeaking();
		for (int i = 0; i < 5; i++) {
			CreateCube ();
		}

	}

	protected override void OnNeigborSparkingParticles()
	{
		//base.OnNeighborSparkingParticles();
	}
}
