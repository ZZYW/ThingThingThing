using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSuperviser : MonoBehaviour {

	private static bool created = false;

	void Awake () {
		if (!created) {
			DontDestroyOnLoad (this.gameObject);
			created = true;
			Debug.Log ("Awake: " + this.gameObject);
		}
	}

	void Start () {
		InvokeRepeating ("RestartAndGC", 600f, 600f);
	}

	void RestartAndGC () {
		SceneManager.LoadScene (0);
		System.GC.CollectionCount (int.MaxValue);
	}
}