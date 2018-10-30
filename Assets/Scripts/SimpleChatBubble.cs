using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleChatBubble : MonoBehaviour {

	public Text text;
	public Image bubble;

	public Transform host;
	public Vector3 offsetPos = new Vector3 ();

	void Start () {

	}

	// Update is called once per frame
	void Update () {
		transform.position = host.position + offsetPos;
		transform.LookAt (Camera.main.transform);
	}

	void OnDisable () {
		CancelInvoke ();
	}

	public void Speak (string content) {

		text.enabled = true;
		bubble.enabled = true;

		text.text = content;

		Invoke ("Disappear", 2f);
	}

	void Disappear () {
		text.enabled = false;
		bubble.enabled = false;
	}
}