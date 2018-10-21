using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FraidyCatMovement : Thing {


	protected override void OnMeetingSomeone(GameObject other){
		
		transform.Translate (new Vector3 (0,0,1));
	}

}
