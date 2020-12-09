using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderSwapper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Swap", 2);
    }

    void Swap()
    {
        var allrends = GameObject.FindObjectsOfType<Renderer>();
        foreach (var rend in allrends)
        {            
            int i = Random.Range(0, 3);
            Color[] colors = new Color[] { Color.red, Color.green, Color.blue };
            
            rend.material = ColorManager.instance.GetMaterial();
            rend.material.color = Color.HSVToRGB(Random.Range(0f, 1f), 1f, 1f); //colors[i];
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
