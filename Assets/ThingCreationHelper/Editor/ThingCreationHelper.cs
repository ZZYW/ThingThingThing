using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class ThingCreationHelper : EditorWindow
{


    string thingName = "";
    string creatorName = "";

    [MenuItem("(｡・`ω´・) Thing-Thing-Thing/Create A New Thing")]
    static void CreateNewThing()
    {
        EditorWindow.GetWindow(typeof(ThingCreationHelper));
    }


    void OnGUI()
    {
        //GUILayout.Label("Name of the THING", EditorStyles.boldLabel);
        thingName = EditorGUILayout.TextField("Name of the THING", thingName);
        creatorName = EditorGUILayout.TextField("Name of the Creator", creatorName);


        if (GUILayout.Button("Generate my THING"))
        {
            CreateMyDir();
            Debug.Log(thingName + "," + creatorName);
        }



        //groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        //myBool = EditorGUILayout.Toggle("Toggle", myBool);
        //myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        //EditorGUILayout.EndToggleGroup();
    }

    void CreateMyDir()
    {
        string dirPath = Path.Combine(Application.dataPath, "CREATORS/" + creatorName);
        Debug.Log("creating folder at path: " + dirPath);
        Directory.CreateDirectory(dirPath);
    }
}
