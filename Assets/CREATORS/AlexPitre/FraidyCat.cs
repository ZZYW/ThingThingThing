using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FraidyCat : Thing {
	 
	AudioSource FCsound;

	protected override void OnMeetingSomeone(GameObject other){
		
		ChangeColor (Color.cyan);
		FCsound = GetComponent<AudioSource> ();
		FCsound.Play (0);

	}



	protected override void OnNeighborSpeaking(){
		ResetColor ();
	}
}
