using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(Thing))]
public class CreatureEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Thing creature = (Thing)target;
        //if (GUILayout.Button("Speak"))
        //{
        //    creature.Speak("AHA!", 2f);
        //}

        //if (GUILayout.Button("Spark"))
        //{
        //    creature.Spark(Color.blue, 10);
        //}


    }

}
#endif