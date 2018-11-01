using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ThingConsole : MonoBehaviour {

    static Text consoleText;
    static int maxLength = 1000;

    static StringBuilder warningString;
    static StringBuilder errorString;
    static StringBuilder normalString;

    static StringBuilder mainStringBuilder;

    static bool disableLogging = false;

    private void Awake () {
        mainStringBuilder = new StringBuilder ();
        mainStringBuilder.Capacity = 4000;
        warningString = new StringBuilder ();
        errorString = new StringBuilder ();
        normalString = new StringBuilder ();
        consoleText = GetComponentInChildren<Text> ();
    }

    void Update () {
        if (Input.GetKeyDown (KeyCode.G)) {
            disableLogging = !disableLogging;
        }

        if (Input.GetKeyDown (KeyCode.H)) {
            Destroy (gameObject);
        }
    }

    public static void LogWarning (string content) {
        warningString.Length = 0;
        warningString.AppendFormat ("<color=yellow>{0}</color>\n", content);
        Log (warningString.ToString ());
    }

    public static void LogError (string content) {
        errorString.Length = 0;
        errorString.AppendFormat ("<color=red>{0}</color>\n", content);
        Log (errorString.ToString ());
    }

    public static void Log (string content) {

        if (disableLogging) return;

        normalString.Length = 0;
        normalString.Append ("<color=green><b>ThingThingThing</b></color> -> ");
        normalString.Append (content);

        mainStringBuilder.AppendLine (normalString.ToString ());

        if (mainStringBuilder.Length > maxLength) {
            mainStringBuilder.Remove (0, mainStringBuilder.Length - maxLength);
        }

        consoleText.text = mainStringBuilder.ToString ();

    }
}