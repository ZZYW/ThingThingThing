using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Example_3_Mask_Box : MonoBehaviour 
{
    //////////////////////////////////////////////////////////////////////////////
    //                                                                          // 
    //Variables                                                                 //                
    //                                                                          //               
    //////////////////////////////////////////////////////////////////////////////
    public Color wireColor = Color.black;
    public float wireSize = 1;

    public GameObject box;

    public Material[] sceneMaterials = null;
    //////////////////////////////////////////////////////////////////////////////
    //                                                                          // 
    //Unity Functions                                                           //                
    //                                                                          //               
    //////////////////////////////////////////////////////////////////////////////
	void Update () 
    {
        if (sceneMaterials == null)
            CollectMaterials();
        

        for (int i = 0; i < sceneMaterials.Length; i++)
        {
            if (sceneMaterials[i] == null)
                continue;
            
            //Enable 'Box Mask' keyword
            sceneMaterials[i].DisableKeyword("V_WIRE_DYNAMIC_MASK_OFF");
            sceneMaterials[i].DisableKeyword("V_WIRE_DYNAMI_MASK_PLANE");
            sceneMaterials[i].DisableKeyword("V_WIRE_DYNAMIC_MASK_SPHERE");
            sceneMaterials[i].EnableKeyword("V_WIRE_DYNAMIC_MASK_BOX");

            //Set Wire color
            sceneMaterials[i].SetColor("_V_WIRE_Color", wireColor);
            sceneMaterials[i].SetFloat("_V_WIRE_Size", wireSize < 0 ? 0 : wireSize);



            //Update 'Box Mask' shader parameters
            if (box != null)
            {
                Bounds bounds = box.GetComponent<MeshFilter>().sharedMesh.bounds;

                Vector3 boundMin = Vector3.Scale(bounds.min, box.transform.localScale);
                Vector3 boundMax = Vector3.Scale(bounds.max, box.transform.localScale);

                Matrix4x4 trs = Matrix4x4.TRS(box.transform.position, box.transform.rotation, Vector3.one).inverse;


                sceneMaterials[i].SetVector("_V_WIRE_DynamicMaskBoundsMin", boundMin);
                sceneMaterials[i].SetVector("_V_WIRE_DynamicMaskBoundsMax", boundMax);
                sceneMaterials[i].SetMatrix("_V_WIRE_DynamicMaskTRS", trs);
            }



            if (Application.isPlaying)
                transform.Rotate(Vector3.up, 0.025f);
        }
	}


    //////////////////////////////////////////////////////////////////////////////
    //                                                                          // 
    //Custom Functions                                                          //                
    //                                                                          //               
    //////////////////////////////////////////////////////////////////////////////
    void CollectMaterials()
    {
        List<Material> mats = new List<Material>();

        Renderer[] renderers = FindObjectsOfType<Renderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
            for (int j = 0; j < renderers[i].sharedMaterials.Length; j++)
            {
                if (mats.Contains(renderers[i].sharedMaterials[j]) == false)
                    mats.Add(renderers[i].sharedMaterials[j]);
            }
        }

        sceneMaterials = mats.ToArray();
    }
}
