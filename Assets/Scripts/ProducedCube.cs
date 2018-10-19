using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProducedCube : MonoBehaviour {

    Color c;

    // Use this for initialization
    internal void Init (Color _c) {

        c = _c;

        Renderer rend = GetComponent<Renderer> ();
        Shader shader = Shader.Find ("ThingThingThing/Main");
        rend.material.shader = shader;

        //set shader
        rend.material.SetInt ("_UseRainbowColors", 1);
        //rend.material.SetFloat("_TimeOffset", Random.Range(0.0f, 10.0f));
        //rend.material.SetFloat("_TimeScale", Random.Range(10f, 100f));
        rend.material.SetColor ("_rainbowcolor1", c);
        rend.material.SetColor ("_rainbowcolor2", Color.white);
        rend.material.SetColor ("_rainbowcolor3", c);

    }

}