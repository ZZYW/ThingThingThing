using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetRandomColor : MonoBehaviour
{

    static Color[] colors;

    // Use this for initialization
    void Start()
    {
        if (colors == null)
        {
            colors = new Color[] {
                new Color(1,0,0),
                new Color(0,0,1),
                new Color(1,1,0),
                new Color(0,0,0)
            };
        }

        Color myColor = colors[Random.Range(0, colors.Length)];
        GetComponent<Renderer>().material.SetColor("_Color", myColor);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
