using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JZPig : Thing {

	// Use this for initialization
		
	protected override void TTTAwake(){
		cameraOffset = 30; 
		acceleration = 6; 
		drag = 1f;
		mass = 0.5f;
		getNewDestinationInterval = 3;
		newDestinationRange = 300;
		myCubeColor = new Color (0.45f, 0.2f, 0.25f);
	}

	protected override void TTTStart(){
		Speak ("为什么?");
		InvokeRepeating ("RandomSetDestination", 3f, 2f);
	}
	protected override void OnMeetingSomeone(GameObject other){
		Speak ("Hehe");
		InvokeRepeating ("RandomSetDestination", 2f, 4f);
		InvokeRepeating ("ResetPosition", 4f, 6f);
	}

	protected override void OnLeavingSomeone (GameObject other){
		Speak ("再见!");
		InvokeRepeating ("RandomSetDestination", 5f, 2f);
		SetScale (new Vector3 (15,15,15));
		}
	 

	protected override void OnSunset(){
		myCubeColor = new Color (0.7f, 0f, 0f);

	}
	
	// Update is called once per frame

}
