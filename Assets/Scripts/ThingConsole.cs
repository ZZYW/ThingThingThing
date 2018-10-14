using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ThingConsole : MonoBehaviour {

    static float verticalNormalizedPosition = 0;
    static Text consoleText;
    static ScrollRect scrollRect;
    static int maxLength = 3000;

    static StringBuilder stringBuilder;

    private void Awake () {
        stringBuilder = new StringBuilder ();
        consoleText = GetComponentInChildren<Text> ();
        scrollRect = GetComponentInChildren<ScrollRect> ();
    }


    public static void Log (string content) {

        stringBuilder.Append (content);
        consoleText.text = stringBuilder.ToString ();

        if (stringBuilder.Length > maxLength) {
            stringBuilder.Remove (0, stringBuilder.Length - maxLength);
        }

        scrollRect.verticalNormalizedPosition = verticalNormalizedPosition;

    }
}