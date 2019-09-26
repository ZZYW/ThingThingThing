using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interessant : Thing {

	protected override void TTTStart(){

		settings.mass = 0.1f;
		settings.acceleration = 0.3f;

		InvokeRepeating("RandomSetDestination",10,20);

	}

	protected override void TTTUpdate(){

	}

	protected override void OnMeetingSomeone(GameObject other){
		SetScale(new Vector3(1,1,1)); 
		Speak("Bitte," + other.name);
	}

	protected override void OnNeighborSpeaking(){
		Spark(Color.red, 10);
		CreateCube();
	}


}
