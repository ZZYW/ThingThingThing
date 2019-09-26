using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingMargarita : Thing {

	Rigidbody margaritaRigidBody;
	Vector3 movement;

	Vector3 originalScale;

	//once
	protected override void TTTAwake () {
		// how far the following camera will be from my object
		settings.cameraOffset = 8;

		settings.acceleration = 4f;
		settings.drag = 0f; //the bigger the drag is, the slower your thing moves
		settings.mass = 0.4f;

		settings.getNewDestinationInterval = 5; //in seconds
		settings.newDestinationRange = 40;

		settings.myCubeColor = new Color (1, 1, 1); // red green blue 0-1

		margaritaRigidBody = GetComponent<Rigidbody> ();

	}

	//once
	protected override void TTTStart () {
		//base.TTTStart();
		// everything inside this code block will only be exe once

		Speak ("I imagine, therefore I belong and am free, like L.Durrell said");

		InvokeRepeating ("RandomSetDestination", 0f, 3f);
		originalScale = transform.localScale;
	}

	protected override void TTTUpdate () {
		//base.TTUpdate();
		// everything inside this code block will be exe many many times
		// about ~ 60 times per second
		flying ();

	}

	// below are all events

	protected override void OnMeetingSomeone (GameObject other) {
		//base.OnMeetingSomeone(other);
		PlaySound (24);

		//everything inside this code block will be triggered/called
		//when my thing meets anyone
	}

	protected override void OnLeavingSomeone (GameObject other) {
		//base.OnLeavingSomeone(other);
	}

	protected override void OnSunset () {
		//this code will be triggered when the sun is setting
		//base.OnSunset();
		Spark (Color.blue, 60);
	}

	protected override void OnSunrise () {
		Spark (Color.white, 60);
		Speak ("breakthrough!");
	}

	protected override void OnTouchWater () {
		//base.OnTouchWater();
		SetScale (new Vector3 (40, 40, 40));

	}

	protected override void OnLeaveWater () {
		SetScale (originalScale);
	}

	protected override void OnNeighborSpeaking () {
		Speak ("that is magic!");
	}

	protected override void OnNeigborSparkingParticles () {
		//base.OnNeighborSparkingParticles();
	}

	void flying () {
		movement.Set (0, 0.1f, 0.2f);

		margaritaRigidBody.MovePosition (transform.position + movement * Time.deltaTime);

	}

}