using UnityEngine;
using UnityEditor;
using System;

public class ToonWaterInspector : ShaderGUI
{
    private const string SHALLOW_WATER_COLOR_ID = "Color_1139F668";
    private const string DEEP_WATER_COLOR_ID = "Color_198818EE";
    private const string SPECULAR_COLOR_ID = "Color_77A2EDE9";
    private const string FOAM_COLOR_ID = "Color_626750DD";
    private const string SPECULAR_ID = "Vector1_301E02E2";
    private const string WATER_DEPTH_ID = "Vector1_45F25267";
    private const string REFRACTION_STRENGTH_ID = "Vector1_31894ABB";
    private const string USE_REFRACTION_IN_DEPTH_BASED_WATER_COLOR = "Boolean_2918AA79";
    private const string WAVE_DIRECTION_ID = "Vector2_D26C9C89";
    private const string WAVE_SIZE_ID = "Vector2_1E1B6943";
    private const string NORMALS_STRENGTH_ID = "Vector1_9C73072A";
    private const string SPECULAR_CUTOFF_ID = "Vector1_20DB7652"; 
    private const string FOAM_AMOUNT_ID = "Vector1_854A7D8C";
    private const string FOAM_CUTOFF_ID = "Vector1_E71BB35E";
    private const string FOAM_DIRECTION_ID = "Vector2_D06E76BC";
    private const string FOAM_SCALE_ID = "Vector2_F678228C";
    private const string FRESNEL_POWER_ID = "Vector1_A524D234";
    private const string SPECULAR_EDGES_SMOOTHNESS_FACTOR_ID = "Vector1_B9CC1720";
    private const string REFLECTION_VISIBILITY_ID = "Vector1_3632ABA2";
    private const string REFLECTION_DISTORTION_STRENGHT = "Vector1_738B39CF";
     
    private static bool colorsFoldout = true; 
    private static bool lightningFoldout = false;
    private static bool fresnelFoldout = false;
    private static bool refractionFoldout = false; 
    private static bool foamFoldout = false;
    private static bool wavesFoldout = false;
    private static bool reflectionFoldout = false;

    public override void OnGUI(MaterialEditor editor, MaterialProperty[] properties)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label(Resources.Load<Texture>("TOON_WATER_LOGO"), GUILayout.Width(308), GUILayout.Height(244));
        GUILayout.EndHorizontal();

        //WATER COLORS
        colorsFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(colorsFoldout, "Water Colors");
        if(colorsFoldout)
        {
            MaterialProperty waterDepth = FindProperty(WATER_DEPTH_ID, properties);
            editor.FloatProperty(waterDepth, "Water Depth");

            MaterialProperty shallowColor = FindProperty(SHALLOW_WATER_COLOR_ID, properties);
            editor.ColorProperty(shallowColor, "Shallow Water Color");

            MaterialProperty deepColor = FindProperty(DEEP_WATER_COLOR_ID, properties);
            editor.ColorProperty(deepColor, "Deep Water Color");
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        //LIGHTNING 
        lightningFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(lightningFoldout, "Light settings");
        if(lightningFoldout)
        {
            MaterialProperty specularPower = FindProperty(SPECULAR_ID, properties);
            editor.RangeProperty(specularPower, "Specular Power");

            MaterialProperty specularCutoff = FindProperty(SPECULAR_CUTOFF_ID, properties);
            editor.RangeProperty(specularCutoff, "Specular Cutoff");

            MaterialProperty specularEdgesSmoothnessFactor = FindProperty(SPECULAR_EDGES_SMOOTHNESS_FACTOR_ID, properties);
            editor.RangeProperty(specularEdgesSmoothnessFactor, "Specular Edges Smoothness Factor");

            MaterialProperty specularColor = FindProperty(SPECULAR_COLOR_ID, properties);
            editor.ColorProperty(specularColor, "Specular Color");

            MaterialProperty normalsStrenght = FindProperty(NORMALS_STRENGTH_ID, properties);
            editor.FloatProperty(normalsStrenght, "Normals Strength");
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        //FRESNEL
        fresnelFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(fresnelFoldout, "Fresnel");
        if(fresnelFoldout)
        {
            MaterialProperty fresnelPower = FindProperty(FRESNEL_POWER_ID, properties);
            editor.RangeProperty(fresnelPower, "Fresnel Power");

            if(fresnelPower.floatValue <= 0)
                EditorGUILayout.HelpBox("When Fresnel Power is set to 0 effect is disabled and Planar Reflections will not be visible!", MessageType.Info);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        //REFRACTION
        refractionFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(refractionFoldout, "Refraction");
        if(refractionFoldout)
        {
            MaterialProperty refractionStrenght = FindProperty(REFRACTION_STRENGTH_ID, properties);
            editor.RangeProperty(refractionStrenght, "Refraction Strength");

            MaterialProperty useRefraction = FindProperty(USE_REFRACTION_IN_DEPTH_BASED_WATER_COLOR, properties);
            useRefraction.floatValue = EditorGUILayout.Toggle("Use Refraction In Depth Based Water Color", useRefraction.floatValue > 0) ? 1 : 0;

        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        //REFLECTION 
        reflectionFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(reflectionFoldout, "Planar Reflections");
        if(reflectionFoldout)
        {
            MaterialProperty reflectionVisiblity = FindProperty(REFLECTION_VISIBILITY_ID, properties);
            editor.RangeProperty(reflectionVisiblity, "Reflection Visibility");

            MaterialProperty reflectionDistortion = FindProperty(REFLECTION_DISTORTION_STRENGHT, properties);
            editor.RangeProperty(reflectionDistortion, "Reflection Distortion Strenght");
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        //FOAM 
        foamFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(foamFoldout, "Foam");
        if(foamFoldout)
        {
            MaterialProperty foamColor = FindProperty(FOAM_COLOR_ID, properties);
            editor.ColorProperty(foamColor, "Foam Color");

            MaterialProperty foamAmount = FindProperty(FOAM_AMOUNT_ID, properties);
            editor.FloatProperty(foamAmount, "Foam Amount");

            MaterialProperty foamCutoff = FindProperty(FOAM_CUTOFF_ID, properties);
            editor.FloatProperty(foamCutoff, "Foam Cutoff");

            MaterialProperty foamDirection = FindProperty(FOAM_DIRECTION_ID, properties);
            editor.VectorProperty(foamDirection, "Foam Direction");

            MaterialProperty foamScale = FindProperty(FOAM_SCALE_ID, properties);
            editor.VectorProperty(foamScale, "Foam Scale");
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        //WAVES
        wavesFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(wavesFoldout, "Waves");
        if(wavesFoldout)
        { 
            MaterialProperty waveDir = FindProperty(WAVE_DIRECTION_ID, properties);
            editor.VectorProperty(waveDir, "Wave Direction");

            MaterialProperty waveSize = FindProperty(WAVE_SIZE_ID, properties);
            editor.VectorProperty(waveSize, "Wave Size");
        }
        EditorGUILayout.EndFoldoutHeaderGroup(); 
    } 
}
