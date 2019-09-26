using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProducedCube : MonoBehaviour {

    Color c;
    Material material;

    // Use this for initialization
    internal void Init (Color _c) {
        c = _c;
        material = GetComponent<Renderer> ().material;
        Shader shader = Shader.Find ("ThingThingThing/Main");
        material.shader = shader;
        material.SetInt ("_UseRainbowColors", 1);
        material.SetColor ("_rainbowcolor1", c);
        material.SetColor ("_rainbowcolor2", Color.white);
        material.SetColor ("_rainbowcolor3", c);
    }

    void OnDisable () {
        Destroy(material);
    }

}