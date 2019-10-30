using UnityEngine;

using VacuumShaders.TheAmazingWireframeShader;

using System.Collections.Generic;


public class Example_8_Runtime_Quad_Converter : MonoBehaviour
{
    [Range(0, 1)]
    public float normalCoef = 1;
    float normalCoef_current = 0;

    [Range(0, 1)]
    public float angleCoef = 1;
    float angleCoef_current = 0;

    [Range(0, 1)]
    public float areaCoef = 1;
    float areaCoef_current = 0;


    Material material;
    Mesh originalMesh = null;
    Mesh quadMesh = null;


    // Use this for initialization
    void Start ()
    {   
        if(gameObject.isStatic)
        {
            enabled = false;

            Debug.Log("Static mesh convertion is not possible");

            return;
        }    

        originalMesh = GetComponent<MeshFilter>().sharedMesh;
        if (originalMesh == null)
        {
            Debug.LogWarning("No mesh data.");

            enabled = false;
        }
        else if(originalMesh.triangles.Length / 3 > 21000)
        {
            Debug.LogWarning("Can not convert mesh with more then 21000 triangles.");

            originalMesh = null;
            enabled = false;
        }

        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (originalMesh == null)
            return;

        //If any of the properties has changed, than generate new mesh
        if ((normalCoef_current != normalCoef) ||
           (angleCoef_current != angleCoef) ||
           (areaCoef_current != areaCoef))
        {
            normalCoef_current = normalCoef;
            angleCoef_current = angleCoef;
            areaCoef_current = areaCoef;


            //Do not forget to delete previously generated quad mesh
            if (quadMesh != null)
                DestroyImmediate(quadMesh);


            //Generate new quad mesh based on 'Coef' parameters
            quadMesh = WireframeGenerator.GenerateQuads(originalMesh, normalCoef, angleCoef, areaCoef);


            //Assign new mesh
            if (quadMesh != null)
            {
                GetComponent<MeshFilter>().sharedMesh = quadMesh;


                //Just make wireframe visible
                material.SetColor("_V_WIRE_Color", new Color(1, 0.5f, 0f, 1));
            }
            else
            {
                Debug.Log("houston we have a problem");
            }

        }
    }
}