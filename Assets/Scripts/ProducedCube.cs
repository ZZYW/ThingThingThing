using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProducedCube : MonoBehaviour
{
    
    Color c;


    // Use this for initialization
    internal void Init(Color _c)
    {


        c = _c;

        Renderer rend = GetComponent<Renderer>();
        Shader shader = Shader.Find("Custom/rainbow");
        rend.material.shader = shader;

        //set shader
        rend.material.SetInt("_Rainbow", 1);
        rend.material.SetFloat("_TimeOffset", Random.Range(0.0f, 10.0f));
        rend.material.SetFloat("_TimeScale", Random.Range(10f, 100f));
        rend.material.SetColor("_RainbowColor1", c);
        rend.material.SetColor("_RainbowColor2", Color.white);
        rend.material.SetColor("_RainbowColor3", c);

        Invoke("DeleteMyself", 600);
    }


    void DeleteMyself()
    {
        Destroy(gameObject);
    }






}
