using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowThing : MonoBehaviour {

	public Transform followTarget;
	public float followDistance;
	public float elevation;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (followTarget == null) return;
		transform.position = followTarget.position - (followDistance * followTarget.forward) + new Vector3 (0, elevation, 0);

		transform.LookAt (followTarget);

	}

}