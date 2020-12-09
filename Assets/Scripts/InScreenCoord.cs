using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InScreenCoord : MonoBehaviour
{
    public LineRenderer r, g, b;
    public float length;
    LineRenderer[] lines;
    public float width = 1;
    public Vector3 screenPositon;
    // Start is called before the first frame update
    void Start()
    {
        lines = new LineRenderer[] { r, g, b };
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.ViewportToScreenPoint(screenPositon);
        transform.localRotation = Camera.main.transform.rotation;


        r.SetPositions(new Vector3[] { Vector3.zero, Vector3.right * length });
        g.SetPositions(new Vector3[] { Vector3.zero, Vector3.up * length });
        b.SetPositions(new Vector3[] { Vector3.zero, Vector3.forward * length });

        foreach (var line in lines)
        {
            line.startWidth = width;
            line.endWidth = width;
        }
    }
}
