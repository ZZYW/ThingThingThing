using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevTool : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            string filename =  System.DateTime.Now.ToString();

            filename = filename.Replace("/", "-");
            filename = filename.Replace(" ", "_");
            filename = filename.Replace(":", "-");
            ScreenCapture.CaptureScreenshot(filename + ".png" , 4);
        }
        
    }
}
