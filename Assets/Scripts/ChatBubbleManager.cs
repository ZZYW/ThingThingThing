using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBubbleManager : MonoBehaviour {

	public static ChatBubbleManager main;
	public GameObject chatBubblePrefab;
	public List<SimpleChatBubble> chatBubbleList;

	void Awake () {
		main = this;
	}

	void OnEnable () {
		CameraSwitcher.OnCameraSwitch += ChangeEventCamera;
	}
	void OnDisable () {
		CameraSwitcher.OnCameraSwitch -= ChangeEventCamera;
	}

	public void Init (int number) {
		chatBubbleList = new List<SimpleChatBubble> ();
		for (int i = 0; i < number; i++) {
			//create a new chatbublle from prefab
			GameObject nChatBubble = Instantiate (chatBubblePrefab, Vector3.zero, Quaternion.identity);
			nChatBubble.transform.SetParent (transform, true);
			//set host and offset position from things
			GameObject targetThing = ThingManager.main.AllThings[i];
			SimpleChatBubble simpleChatBubble = nChatBubble.GetComponent<SimpleChatBubble> ();

			//set SimepleChatBubble vital references
			simpleChatBubble.host = targetThing.transform;
     

			//link chatbubble with thing
			targetThing.GetComponent<Thing> ().myChatBubble = simpleChatBubble;

			//add this chatbubble to list
			chatBubbleList.Add (simpleChatBubble);
		}
	}

	void ChangeEventCamera () {
		GetComponent<Canvas> ().worldCamera = Camera.main;
	}
}