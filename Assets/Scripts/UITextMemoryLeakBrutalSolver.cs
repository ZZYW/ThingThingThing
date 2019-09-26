using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITextMemoryLeakBrutalSolver : MonoBehaviour {

	[SerializeField] GameObject TTTConsoleGameobject;
	[SerializeField] GameObject ChatBubbleCanvasGameobject;

	[Header ("Prefab to be reinstantiated")]
	[SerializeField] GameObject TTTConsolePrefab;
	[SerializeField] GameObject ChatBubbleManagerPrefab;

	// public float TConsoleDestroyInterval = 3000;
	public float TConsoleRebornWait = 2;

	// public float ChatBubbleDestroyInterval = 300;
	public float ChatbubbleRebornWait = 10;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("DeleteAllCanvases", 120, 120);
		// InvokeRepeating ("DestroyChatBubbles", ChatBubbleDestroyInterval, ChatBubbleDestroyInterval);
	}

	void DeleteAllCanvases () {
		// DestroyTTTConsole ();
		DestroyChatBubbles ();
	}

	//tttconsole
	// void DestroyTTTConsole () {
	// 	Destroy (TTTConsoleGameobject);
	// 	Invoke ("ReinstTConsole", TConsoleRebornWait);
	// }

	// void ReinstTConsole () {
	// 	TTTConsoleGameobject = GameObject.Instantiate (TTTConsolePrefab, Vector3.zero, Quaternion.identity);
	// }

	//chatbubbles
	void DestroyChatBubbles () {
		Destroy (ChatBubbleCanvasGameobject);
		Invoke ("ReInstChatBubbles", ChatbubbleRebornWait);
	}

	void ReInstChatBubbles () {
		ChatBubbleCanvasGameobject = GameObject.Instantiate (ChatBubbleManagerPrefab, Vector3.zero, Quaternion.identity);
		ChatBubbleCanvasGameobject.GetComponent<ChatBubbleManager> ().Init (ThingManager.main.AllThings.Count);
	}
}