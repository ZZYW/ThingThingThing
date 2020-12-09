using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public static ColorManager instance;
    public Material material;

    float h = 0;

    void Awake()
    {
        instance = this;
    }

    public Color GetColor()
    {
        h += 0.1f;
        if (h > 1) { h = 0; }
        return Color.HSVToRGB(Random.Range(0f, 1f), 1f, 1f);

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    internal Material GetMaterial(float ditherSize, bool animate)
    {
        Material newMat = GameObject.Instantiate(material);
        newMat.SetColor("_ColorA1", GetColor());
        newMat.SetColor("_ColorA2", GetColor());
        newMat.SetColor("_ColorB1", GetColor());
        newMat.SetColor("_ColorB2", GetColor());
        newMat.SetFloat("_DitherSize", ditherSize);
        newMat.SetFloat("_OffsetTime", Random.Range(0f, 10f));
        newMat.SetInt("_AnimatorSwitch", animate ? 1 : 0);


        return newMat;
    }

    // internal void DressUpThing(GameObject target)
    // {
    //     target.GetComponent<Renderer>().material = material;
    //     target.GetComponent<Renderer>().material.SetColor("_Color", GetColor());
    //     target.GetComponent<Renderer>().material.SetColor("_Color2", GetColor());
    //     target.GetComponent<Renderer>().material.SetFloat("_UVMultplier", Random.Range(0.1f, 0.6f));
    // }
}
