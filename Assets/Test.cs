using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour {

	const int n = 30;
	GameObject[] ob = new GameObject[n];
	SimpleChatBubble[] bubbles;

	public GameObject bubblePrefab;

	void Start () {
		for (int i = 0; i < n; i++) {
			ob[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
			ob[i].transform.position = new Vector3 (i * 5, 0, 0);

			GameObject chatBubbleGO = GameObject.Instantiate (bubblePrefab, Vector3.zero, Quaternion.identity);
			chatBubbleGO.transform.SetParent (transform);
			chatBubbleGO.transform.position = ob[i].transform.position+Vector3.up;

		}
	}

}