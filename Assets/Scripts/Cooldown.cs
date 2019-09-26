using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooldown {

	public bool inCD;
	public float timeStamp;
	public float cdTime;

	public Cooldown (float cooldownTime) {
		this.cdTime = cooldownTime;
	}

	public void Check () {
		if (Time.time - this.timeStamp > this.cdTime && this.inCD) {
			this.inCD = false;
		}
	}
	public void GoCooldown () {
		this.inCD = true;
		this.timeStamp = Time.time;
	}
}