using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleChatBubble : MonoBehaviour {

	public Transform host;
	//public Vector3 offsetPos;

	[SerializeField] private Text text;
	[SerializeField] private Image bubble;

	// public Vector3 defaultOffsetPos;

	void Start () {
		Init ();
	}

	void Init () {
		Disappear ();
	}

	void Update () {
		transform.position = host.position + Vector3.up * host.localScale.y * 3;
		transform.LookAt (CameraSwitcher.main.ActiveCam.position);
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