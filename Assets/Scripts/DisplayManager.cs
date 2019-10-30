using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayManager : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Screen.SetResolution(1080, 1920 , false);
        Debug.Log("displays connected: " + Display.displays.Length);
        // Display.displays[0] is the primary, default display and is always ON.
        // Check if additional displays are available and activate each.
        if (Display.displays.Length > 1)
            Display.displays[1].Activate();
        if (Display.displays.Length > 2)
            Display.displays[2].Activate();
    }
}
