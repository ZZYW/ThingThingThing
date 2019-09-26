using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ThingCreationHelper : EditorWindow {

    string thingName = "";
    string creatorName = "";
    Object prefab;

    public Object speechBubblePrefab;
    public Object particleSystemPrefab;

    [MenuItem ("(｡・`ω´・) ThingThingThing/Helper")]
    static void CreateNewThing () {
        EditorWindow.GetWindow (typeof (ThingCreationHelper));
    }

    [MenuItem ("(｡・`ω´・) ThingThingThing/Open Main World Scene")]
    static void OpenMainWorldScene () {
        UnityEditor.SceneManagement.EditorSceneManager.OpenScene (Application.dataPath + "/Scenes/TTTWorldScene.unity");
    }

    [MenuItem ("(｡・`ω´・) ThingThingThing/Open Test Scene")]
    static void OpenTestScene () {
        UnityEditor.SceneManagement.EditorSceneManager.OpenScene (Application.dataPath + "/Test/TEST.unity");
    }

    void OnGUI () {
        GUILayout.Label ("Prepare my directory", EditorStyles.boldLabel);
        thingName = EditorGUILayout.TextField ("Name of the THING", thingName);
        creatorName = EditorGUILayout.TextField ("Name of the Creator", creatorName);

        if (GUILayout.Button ("Generate")) {
            CreateMyDir ();
            CopyWorldScene (thingName, creatorName);
            CreateMaterial (thingName, creatorName);
            Debug.Log (thingName + "," + creatorName);
        }

        /* 
                    GUILayout.Label("Prepare my THING", EditorStyles.boldLabel);
                    prefab = EditorGUILayout.ObjectField(prefab, typeof(GameObject), true);

                    if (GUILayout.Button("Initialize my THING"))
                    {
                        AttachSpeechBubble(prefab);
                        AttachParticleSystem(prefab);
                    }
        */

    }

    void CreateMyDir () {
        string dirPath = Path.Combine (Application.dataPath, "CREATORS/" + creatorName);
        Debug.Log ("creating folder at path: " + dirPath);
        Directory.CreateDirectory (dirPath);
    }

    void CopyWorldScene (string thingName, string creatorName) {
        string original = Application.dataPath + "/Scenes/TTTWorldScene.unity";
        string dest = Application.dataPath + "/CREATORS/" + creatorName + "/TTTWorldScene_" + creatorName + ".unity";
        FileUtil.CopyFileOrDirectory (original, dest);
    }

    void CreateMaterial (string thingName, string creatorName) {
        string destPath = "Assets/CREATORS/" + creatorName + "/Material_" + creatorName + ".mat";
        AssetDatabase.CreateAsset (new Material (Shader.Find ("ThingThingThing/Main")), destPath);
    }

    void AttachSpeechBubble (Object obj) {
        GameObject sb = (GameObject) PrefabUtility.InstantiatePrefab (Resources.Load ("Chat Balloon"));
        GameObject gobj = (GameObject) obj;
        sb.transform.parent = gobj.transform;
        sb.transform.localPosition = Vector3.zero;
    }

    void AttachParticleSystem (Object obj) {
        GameObject sb = (GameObject) PrefabUtility.InstantiatePrefab (Resources.Load ("Particle Explode"));
        GameObject gobj = (GameObject) obj;
        sb.transform.parent = gobj.transform;
        sb.transform.localPosition = Vector3.zero;
    }

}