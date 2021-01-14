using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class helper : MonoBehaviour
{
    public bool doit;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (doit)
        {
            var lines = GetComponentsInChildren<LineRenderer>();
            foreach (var line in lines)
            {
                for (int i = 0; i < line.positionCount; i++)
                {
                    var pos1 = line.GetPosition(i);
                    line.SetPosition(i, pos1 - new Vector3(0.5f, 0.5f, 0.5f));
                }
            }


            doit = false;
        }
    }
}
