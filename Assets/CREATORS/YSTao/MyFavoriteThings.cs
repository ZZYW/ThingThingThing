using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyFavoriteThings : Thing {

	protected override void TTTStart() {
		settings.mass = 0.1f;
		settings.acceleration = 1;
		settings.drag = 5;
		settings.cameraOffset = 20;
		settings.chatBubbleOffsetHeight = 20;
		settings.newDestinationRange = 100;
		settings.myCubeColor = Color.red;
		Speak("Debug Complete.");
		InvokeRepeating("RandomSetDestination", 2, 2);
	}

	protected override void TTTUpdate() {
		if (settings.mass < 5) {
			settings.mass += 0.01f;
		}
		if (settings.acceleration < 10) {
			settings.acceleration += 0.01f;
		}
	}

	protected override void OnMeetingSomeone(GameObject other) {
		Speak("Grrrrrrrr!");
		PlaySound(75);
		Spark(Color.yellow, 20);
	}
	
	protected override void OnTouchWater() {
		PlaySound(82);
		for (int i=0; i<5; i++) {
			CreateCube();
		}
	}

	protected override void OnNeigborSparkingParticles() {
		Speak("These are a few of my favorite things.");
		PlaySound(26);
	}

}
