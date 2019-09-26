using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PixelStretchingEffect : MonoBehaviour {


    public Material mat;

    float x;
    float y;

    public float X {
        get {
            return x;
        }

        set {
            x = value;
            mat.SetFloat ("_x", x);
        }
    }

    public float Y {
        get {
            return y;
        }

        set {
            y = value;
            mat.SetFloat ("_y", y);
        }
    }



    void OnRenderImage (RenderTexture src, RenderTexture dest) {
        Graphics.Blit (src, null, mat);
    }

}