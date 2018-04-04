using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TOD_Sky))]
class TOD_EditorInspector : Editor
{
    private Rect rect = new Rect(0, 0, 300, 150);

    public override void OnInspectorGUI()
    {
        #if UNITY_3_0 || UNITY_3_1 || UNITY_3_2 || UNITY_3_3 || UNITY_3_4 || UNITY_3_5
        EditorGUIUtility.LookLikeInspector();
        #endif

        if (GUILayout.Button("Choose Preset"))
        {
            EditorWindow.GetWindowWithRect(typeof(TOD_EditorWindow), rect, true, "Choose Preset");
        }

        DrawDefaultInspector();
    }
}
