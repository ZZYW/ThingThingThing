using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FraidyCat : Thing {
	 
    ParticleSystem particleSys;
    ParticleSystem.MinMaxGradient startColor;

    protected override void TTTStart()
    {
        particleSys= GetComponentInChildren<ParticleSystem>();
        startColor = particleSys.main.startColor;
        transform.position = transform.parent.position;
    }

    protected override void OnMeetingSomeone(GameObject other){
        var mainModule = particleSys.main;
        mainModule.startColor = Color.black;
        PlaySound(0);
	}

	protected override void OnNeighborSpeaking(){
        var mainModule = particleSys.main;
        mainModule.startColor = startColor;
    }
}
