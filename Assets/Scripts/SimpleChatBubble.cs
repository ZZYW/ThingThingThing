using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleChatBubble : MonoBehaviour {

	public Text text;
	public Image bubble;

	public Transform host;

	[SerializeField] private Vector3 offsetPos;

	// public Vector3 defaultOffsetPos;

	void Start () {
		Init();
	}

	void Init () {
		Disappear ();
	}

	public void SetOffsetPos (Vector3 vec3) {
		offsetPos = vec3;
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