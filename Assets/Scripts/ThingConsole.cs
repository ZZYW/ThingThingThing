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
    static int maxLength = 1000;

    static StringBuilder warningString;
    static StringBuilder errorString;

    static StringBuilder stringBuilder;

    static bool disableLogging = false;

    static string logFilePath;
    static int writtenLength = 0;

    private void Awake () {
        // logFilePath = Application.dataPath + "/StreamingAssets/tttLog.txt";
        // if (File.Exists (logFilePath)) {
        //     File.Delete (logFilePath);
        //     Debug.Log ("exist");
        // } else {
        //     File.Create (logFilePath);
        // }
        stringBuilder = new StringBuilder ();
        stringBuilder.Capacity = 4000;
        warningString = new StringBuilder ();
        errorString = new StringBuilder ();
        consoleText = GetComponentInChildren<Text> ();
        scrollRect = GetComponentInChildren<ScrollRect> ();
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

        // using (StreamWriter writer = new StreamWriter (logFilePath, true)) {
        //     writtenLength += content.Length;
        //     if (writtenLength > maxLength * 3) {
        //         File.WriteAllText(logFilePath,"");
        //         writtenLength = 0;
        //     }
        //     writer.WriteLine (content);
        // }

        // using(StreamReader reader = new StreamReader(logFilePath)){
        //     consoleText.text = reader.ReadToEnd();
        // }

        stringBuilder.AppendLine (content);

        if (stringBuilder.Length > maxLength) {
            stringBuilder.Remove (0, stringBuilder.Length - maxLength);
        }

         consoleText.text = stringBuilder.ToString ();

    }
}