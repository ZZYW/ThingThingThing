using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleChatBubble : MonoBehaviour {

	public Text text;
	public Image bubble;

	float gapTime;
	// Use this for initialization
	void Start () {
		gapTime = Random.Range(0.2f,1f);
		StartCoroutine (ConstantlyTaking ());
		// text = GetComponentInChildren<Text> ();
		// bubble = GetComponent<Image>();
	}

	// Update is called once per frame
	void Update () {

	}

	void OnDisable () {
		StopAllCoroutines ();
	}

	IEnumerator ConstantlyTaking () {
		while (true) {
			Speak (Random.Range (99999, 9999999).ToString ());
			yield return new WaitForSeconds (gapTime);
		}
	}
	public void Speak (string content) {
		// Debug.Log("speakingL " + content);
		text.text = content;

		Vector2 bubbleSize = new Vector2();
		bubbleSize.x = text.preferredWidth;
		bubbleSize.y = text.preferredHeight;
		
		bubble.rectTransform.sizeDelta=bubbleSize;
		// bubbleSize.x = text.tex
		
		//Invoke ("Disappear", 2);
	}

	void Disappear () {
		text.enabled = false;
		bubble.enabled = false;
	}
}