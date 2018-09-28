using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;



    public class ThingCreationHelper : EditorWindow
    {


        string thingName = "";
        string creatorName = "";
        Object prefab;

        public Object speechBubblePrefab;
        public Object particleSystemPrefab;

        [MenuItem("(｡・`ω´・) Thing-Thing-Thing/Create A New Thing")]
        static void CreateNewThing()
        {
            EditorWindow.GetWindow(typeof(ThingCreationHelper));
        }


        void OnGUI()
        {
            GUILayout.Label("Prepare my directory", EditorStyles.boldLabel);
            thingName = EditorGUILayout.TextField("Name of the THING", thingName);
            creatorName = EditorGUILayout.TextField("Name of the Creator", creatorName);

            if (GUILayout.Button("Generate"))
            {
                CreateMyDir();
                CopyWorldScene(thingName, creatorName);
                Debug.Log(thingName + "," + creatorName);
            }

            GUILayout.Label("Prepare my THING", EditorStyles.boldLabel);
            prefab = EditorGUILayout.ObjectField(prefab, typeof(GameObject), true);

            if (GUILayout.Button("Initialize my THING"))
            {
                AttachSpeechBubble(prefab);
                AttachParticleSystem(prefab);
            }


        }

        void CreateMyDir()
        {
            string dirPath = Path.Combine(Application.dataPath, "CREATORS/" + creatorName);
            Debug.Log("creating folder at path: " + dirPath);
            Directory.CreateDirectory(dirPath);
        }

        void CopyWorldScene(string thingName, string creatorName)
        {
            string original = Application.dataPath + "/Scenes/TTTWorldScene.unity";
            string desk = Application.dataPath + "/CREATORS/" + creatorName + "/TTTWorldScene_" + creatorName + ".unity";
            FileUtil.CopyFileOrDirectory(original, desk);
        }

        void AttachSpeechBubble(Object obj)
        {
            GameObject sb = (GameObject)PrefabUtility.InstantiatePrefab(Resources.Load("Chat Balloon"));
            GameObject gobj = (GameObject)obj;
            sb.transform.parent = gobj.transform;
            sb.transform.localPosition = Vector3.zero;
        }

        void AttachParticleSystem(Object obj)
        {
            GameObject sb = (GameObject)PrefabUtility.InstantiatePrefab(Resources.Load("Particle Explode"));
            GameObject gobj = (GameObject)obj;
            sb.transform.parent = gobj.transform;
            sb.transform.localPosition = Vector3.zero;
        }

    }

