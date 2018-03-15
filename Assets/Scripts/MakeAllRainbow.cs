using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeAllRainbow : MonoBehaviour
{
    public Shader rainbowShader;

    List<GameObject> all;


    // Use this for initialization
    void Start()
    {
        all = new List<GameObject>();

        GameObject[] allGO = FindObjectsOfType<GameObject>();
        foreach (GameObject go in allGO)
        {
            if (go.activeInHierarchy)
            {
                all.Add(go);
            }
        }

        foreach(GameObject g in all)
        {
            Renderer rend = g.GetComponent<Renderer>();
            if(rend!=null && g.tag != "norainbow")
            {
                rend.material.shader = rainbowShader;
                rend.material.SetInt("_Rainbow", 1);
                rend.material.SetFloat("_TimeOffset", Random.Range(0.0f, 10.0f));
                rend.material.SetFloat("_TimeScale", Random.Range(10f, 100f));
                rend.material.SetColor("_RainbowColor1", Color.red);
                rend.material.SetColor("_RainbowColor2", Color.yellow);
                rend.material.SetColor("_RainbowColor3", Color.blue);
            }
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}
