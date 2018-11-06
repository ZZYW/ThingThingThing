using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaBear : Thing {

	protected override void TTTStart(){
		settings.mass = 2.1f;
		settings.drag = 1.1f;
		settings.acceleration = 3;
		settings.chatBubbleOffsetHeight = 2;
		InvokeRepeating("SetRandomTarget", 0, 3);
	}

	protected override void TTTUpdate(){
	}

	protected override void OnMeetingSomeone(GameObject other){
		Speak("你知道小章鱼去哪了吗？");
	}

}
