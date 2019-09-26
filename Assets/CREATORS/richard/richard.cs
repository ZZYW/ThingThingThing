using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class richard : Thing {

	protected override void TTTAwake () {
		settings.cameraOffset = 10;

		settings.acceleration = 10;
		settings.drag = 5f;
		settings.mass = 0.2f;

		settings.getNewDestinationInterval = 5;
		settings.newDestinationRange = 100;

		settings.myCubeColor = new Color (0.8f, 0.2f, 0.1f);

	}

	protected override void TTTStart () {
		Speak ("I am alive!!");
		InvokeRepeating ("RandomSetDestination", 1f, 5f);
	}

	protected override void OnMeetingSomeone (GameObject other) {
		Speak ("Salut! I am Richard, the Cloud. $%#kfjladkjf&8420");
		CreateCube ();
	}

	protected override void OnLeavingSomeone (GameObject other) {
		PlaySound (82);
	}

	protected override void OnNeigborSparkingParticles () {
		Spark (Color.white, 50);
	}

}