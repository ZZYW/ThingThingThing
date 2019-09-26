using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.C) && Input.GetKey(KeyCode.LeftShift)) {
            ScreenCapture.CaptureScreenshot(System.DateTime.Now.Day + "-" + System.DateTime.Now.Hour + "-" + System.DateTime.Now.Minute + "-" + System.DateTime.Now.Second + ".png", 3);
        }
	}
}
