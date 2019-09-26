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
            if(rend!=null && g.tag != "norainbow" && Random.Range(0f,1f)>0.7f)
            {
                rend.material.shader = rainbowShader;
                rend.material.SetInt("_Rainbow", 1);
                rend.material.SetFloat("_TimeOffset", Random.Range(0.0f, 10.0f));
                rend.material.SetFloat("_TimeScale", Random.Range(10f, 100f));
                rend.material.SetColor("_RainbowColor1", new Color32(249,54,54,255));
                rend.material.SetColor("_RainbowColor2", Color.green);
                rend.material.SetColor("_RainbowColor3", new Color32(0,29,255,255));

                if(Random.Range(0f,1f) > 0.9)
                {
                    rend.material.SetFloat("_NoiseScaler", Random.Range(0.1f,0.5f));
                }
            }
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}
