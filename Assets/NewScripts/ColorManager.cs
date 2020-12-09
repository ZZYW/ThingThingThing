using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public static ColorManager instance;
    public Material material;


    void Awake()
    {
        instance = this;
    }

    public Color GetColor()
    {
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

    internal Material GetMaterial()
    {
        Material newMat = GameObject.Instantiate(material);
        newMat.SetColor("_Color", GetColor());
      
      
        return newMat;
    }

    internal void DressUpThing(GameObject target)
    {
        target.GetComponent<Renderer>().material = material;
        target.GetComponent<Renderer>().material.SetColor("_Color", GetColor());
        target.GetComponent<Renderer>().material.SetColor("_Color2", GetColor());
        target.GetComponent<Renderer>().material.SetFloat("_UVMultplier", Random.Range(0.1f, 0.6f));
    }
}
