using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rainbowChicken : Thing {

	protected override void TTTAwake()
	{
		//camera
		settings.cameraOffset = 15; //distance from camera to object center on 3rd personn camera following mode

		//Movement
		settings.acceleration = Random.Range(2.5f, 3f); //use Random.range to get a random number within a range
		settings.drag = 1.8f; // the bigger, the slower
		settings.mass = 0.2f; // the bigger, the heavier, the more acceleration it needs to get this moving, also can push away lighter THINGS
		settings.getNewDestinationInterval = 5; //how often to get a new target to run to, in (seconds)
		settings.newDestinationRange = 40; // how far the new destination could be 

		//color
		settings.myCubeColor = Color.red; //cube's color produced by you
	}

	//only run once at the start
	protected override void TTTStart()
	{
		//we recommend keep this one, or you can write your own rule of moving
		InvokeRepeating("RandomSetDestination", 0, settings.getNewDestinationInterval); //call "RandomSetDestination" method every "getNewDestinationInterval" seconds
	}

	//run about 40~60 times per second, infinitely
	protected override void TTTUpdate()
	{

	}

	protected override void OnMeetingSomeone(GameObject other)
	{
		Speak("I met " + other.name, 2f);
	}

	protected override void OnLeavingSomeone(GameObject other)
	{
		Spark(Color.red, 15);
	}

	protected override void OnNeighborSpeaking()
	{
		PlaySound(93);
	}

	protected override void OnTouchWater()
	{

	}

	protected override void OnLeaveWater()
	{

	}

	protected override void OnNeigborSparkingParticles()
	{
		Speak("Hey You sparked!");
	}

	protected override void OnSunset()
	{

	}

	protected override void OnSunrise()
	{

	}

}

