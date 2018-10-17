using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildrenCounter : MonoBehaviour {

//TODO: children number control
	public int ChildrenNumber;

	void Update () {
		ChildrenNumber = transform.childCount;
	}
}