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
            
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
