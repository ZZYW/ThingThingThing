using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSuperviser : MonoBehaviour {

	void Update () {

		System.GC.CollectionCount (1000);

		if (Input.GetKeyDown (KeyCode.LeftShift) && Input.GetKey (KeyCode.R)) {
			SceneManager.LoadScene (0);
		}

	}
}