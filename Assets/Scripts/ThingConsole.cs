using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ThingConsole : MonoBehaviour {

    static Text consoleText;

    static StringBuilder warningString;
    static StringBuilder errorString;
    static StringBuilder normalString;
    static StringBuilder mainStringBuilder;

    static bool disableLogging = false;

    static bool cooldown;
    static float lastTimelogStamp;
    float cooldownTime = 0.1f; //minimal log interval is 1s;
    static int lineCount = 0;
    const int maxLineCount = 13;

    Canvas canvas;

    private void Awake () {

        if (Display.displays.Length < 3 && !Application.isEditor) {
            Debug.Log ("destroying thingconsole");
            Destroy (gameObject);
        }

        mainStringBuilder = new StringBuilder ();
        mainStringBuilder.Capacity = 4000;
        warningString = new StringBuilder ();
        errorString = new StringBuilder ();
        normalString = new StringBuilder ();
        consoleText = GetComponentInChildren<Text> ();
        canvas = GetComponentInChildren<Canvas> ();
    }

    void Update () {
        if (Input.GetKeyDown (KeyCode.G)) {
            disableLogging = !disableLogging;
        }

        if (Input.GetKeyDown (KeyCode.H)) {
            Destroy (gameObject);
        }

        if (Time.time - lastTimelogStamp > cooldownTime && cooldown) {
            cooldown = false;
        }

    }

    void GetMemorySize (Object obj) {
        using (Stream s = new MemoryStream ()) {
            BinaryFormatter formatter = new BinaryFormatter ();
            formatter.Serialize (s, obj);
            Debug.Log (s.Length);
        }
    }

    public static void LogWarning (string content) {
        if (cooldown) return;
        warningString.Length = 0;
        warningString.AppendFormat ("<color=yellow>{0}</color>\n", content);
        Log (warningString.ToString ());
    }

    public static void LogError (string content) {
        if (cooldown) return;
        errorString.Length = 0;
        errorString.AppendFormat ("<color=red>{0}</color>\n", content);
        Log (errorString.ToString ());
    }

    public static void Log (string content) {        
        if (disableLogging) return;
        if (cooldown) { return; }
        if(consoleText!=null) if (consoleText.text == null) return;

        normalString.Length = 0;
        normalString.AppendFormat ("[<i>{0}</i>]<color=cyan> <b>ThingThingThing</b> </color> -> {1}", System.DateTime.Now.ToString (), content);

        mainStringBuilder.AppendLine (normalString.ToString ());
        lineCount++;

        if (lineCount > maxLineCount) {
            int indexOfFirstLineBreak = mainStringBuilder.ToString ().IndexOf ('\n');
            mainStringBuilder.Remove (0, indexOfFirstLineBreak + 1);
            lineCount--;
        }

        consoleText.text = mainStringBuilder.ToString ();

        //cooldown stuff
        cooldown = true;
        lastTimelogStamp = Time.time;

    }

}