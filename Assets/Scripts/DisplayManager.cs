using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayManager : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

        Debug.Log("Detected display number: " + Display.displays.Length);

        for (int i = 0; i < Display.displays.Length; i++)
        {
            try
            {
                Debug.Log("trying to active display number: " + i);
                Display.displays[i].Activate();
              
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }
        }

    }

}
