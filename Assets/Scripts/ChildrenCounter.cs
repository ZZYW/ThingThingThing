using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildrenCounter : MonoBehaviour {

	//TODO: children number control
	public int ChildrenNumber;
	public int maxChildrenNumber = 500;

	public List<GameObject> list;

	void Awake () {
		list = new List<GameObject> ();
	}

	void Update () {
		ChildrenNumber = transform.childCount;
		
		if (list.Count > maxChildrenNumber) {
			for (int i = 0; i < list.Count - maxChildrenNumber; i++) {
				Destroy (list[i]);
				list.RemoveAt (i);
			}
		}

	}
}