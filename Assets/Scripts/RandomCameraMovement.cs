using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCameraMovement : MonoBehaviour {

	public float circlingSpeed;
	public float circleR;
	public float yLevel;
	public Vector3 offsetMovingSpeed;
	public float noiseSeedXIncremental;
	public float noiseSeedYIncremental;
	public float noiseSeedZIncremental;
	public Transform worldCenter;
	public bool alwaysLookAtCenter;

	float noiseSeedX;
	float noiseSeedY;
	float noiseSeedZ;
	float theta;

	void Start () {
		noiseSeedX = Random.Range (0f, 100f);
		noiseSeedY = Random.Range (0f, 100f);
		noiseSeedZ = Random.Range (0f, 100f);
	}

	void Update () {

		if (alwaysLookAtCenter) {
			transform.LookAt (worldCenter);
		}

		Vector3 offset = new Vector3 (
			(Mathf.PerlinNoise (noiseSeedX, 0) - 0.5f) * offsetMovingSpeed.x,
			(Mathf.PerlinNoise (noiseSeedY, 0) - 0.5f) * offsetMovingSpeed.y,
			(Mathf.PerlinNoise (noiseSeedZ, 0) - 0.5f) * offsetMovingSpeed.z
		);

		Vector3 positionOnCircle = new Vector3 (
			worldCenter.position.x + (circleR + offset.x) * Mathf.Cos (theta),
			yLevel + offset.y,
			worldCenter.position.y + (circleR + offset.z) * Mathf.Sin (theta)
		);

		//move
		transform.position = positionOnCircle;

		IncrementNoiseSeed ();
		IncrementTheta ();

	}

	void IncrementNoiseSeed () {

		noiseSeedX += noiseSeedXIncremental;
		noiseSeedY += noiseSeedYIncremental;
		noiseSeedZ += noiseSeedZIncremental;
	}

	void IncrementTheta () {
		theta += circlingSpeed;
	}

}