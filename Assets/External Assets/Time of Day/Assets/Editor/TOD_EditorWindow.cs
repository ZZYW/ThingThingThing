using UnityEngine;
using UnityEditor;

public class TOD_EditorWindow : EditorWindow
{
    public enum AtmospherePreset
    {
        Default,
        Tropical,
        Nordic,
        Mars,
        Fantasy
    }

    AtmospherePreset preset;
    TOD_Sky sky;

    protected void OnGUI()
    {
        GUILayout.Label("Please note: This resets the parameters of the\ncurrently selected TOD_Sky component!");

        GUILayout.FlexibleSpace();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Instance:", GUILayout.Width(100));
        GUILayout.Label(sky ? sky.name : "None");
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Preset:", GUILayout.Width(100));
        preset = (AtmospherePreset)EditorGUILayout.EnumPopup(preset);
        GUILayout.EndHorizontal();

        GUILayout.FlexibleSpace();

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Apply", GUILayout.Width(100)) && sky)
        {
            PrefabUtility.ResetToPrefabState(sky);
            switch (preset)
            {
                case AtmospherePreset.Tropical:
                    sky.Light.SkyColoring = 0.75f;
                    sky.Light.CloudColoring = 1.0f;
                    sky.Cycle.Longitude = -75;
                    sky.Cycle.Latitude = 20;
                    sky.Cycle.UTC = -5;
                    break;
                case AtmospherePreset.Nordic:
                    sky.Light.SkyColoring = 0;
                    sky.Light.CloudColoring = 0.25f;
                    sky.Cycle.Longitude = 5;
                    sky.Cycle.Latitude = 58;
                    sky.Cycle.UTC = +1;
                    break;
                case AtmospherePreset.Mars:
                    sky.Atmosphere.ScatteringColor = new Color32(255, 104, 39, 255);
                    sky.Atmosphere.RayleighMultiplier = 0.1f;
                    sky.Night.SkyMultiplier = 0.25f;
                    sky.Light.SkyColoring = 0.0f;
                    sky.Clouds.Density = 0.1f;
                    break;
                case AtmospherePreset.Fantasy:
                    sky.Atmosphere.ScatteringColor = new Color32(177, 93, 255, 255);
                    break;
            }
            EditorUtility.SetDirty(sky);
        }
        GUILayout.EndHorizontal();
    }

    protected void OnFocus()
    {
        var go = Selection.activeGameObject;
        sky = go ? go.GetComponent<TOD_Sky>() : null;
    }

    protected void OnSelectionChange()
    {
        OnFocus();
    }
}
