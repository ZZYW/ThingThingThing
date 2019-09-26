using System.Collections;
using UnityEngine;

public class RotateAround : MonoBehaviour {
	//Rotates around this GameObject 
	public GameObject rotateAroundObject;

	//Speed of rotation
	public float speed = 10f;

	//Axis of the rotation
	public Vector3 axis = Vector3.up;

	private Vector3 initPos;
	public Transform followThing;
	public bool followSomething = false;
	//Use this for initialization
	void Start () {
		initPos = this.gameObject.transform.position;
	}

	//Update is called once per frame
	void Update () {
		if (rotateAroundObject == null)
			rotateAroundObject = this.gameObject;

		this.transform.RotateAround (rotateAroundObject.transform.position, axis, speed * Time.deltaTime);

	}

	void LateUpdate () {
		if (followSomething && followThing != null) {
			this.transform.position = new Vector3 (followThing.transform.position.x, initPos.y, followThing.transform.position.z);
			//this.transform.rotation = followThing.transform.rotation;
		}
	}

	public void followThisThing (bool onOrOff, Transform target) {
		followSomething = onOrOff;
		followThing = target;
	}

	public void setToOrigPos () {
		this.transform.position = initPos;
		followSomething = false;
	}
}