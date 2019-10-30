using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Example_2_Mask : MonoBehaviour 
{
    //////////////////////////////////////////////////////////////////////////////
    //                                                                          // 
    //Variables                                                                 //                
    //                                                                          //               
    //////////////////////////////////////////////////////////////////////////////
    public Material[] sceneMaterials = null;

    //////////////////////////////////////////////////////////////////////////////
    //                                                                          // 
    //Unity Functions                                                           //                
    //                                                                          //               
    //////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        CollectMaterials();
    }

    void Update () 
    {
        if (sceneMaterials == null)
            CollectMaterials();

        for (int i = 0; i < sceneMaterials.Length; i++)
        {
            if (sceneMaterials[i] != null)
            {
                //Update Mask Position and Normal
                sceneMaterials[i].SetVector("_V_WIRE_DynamicMaskWorldPos", transform.position);
                sceneMaterials[i].SetVector("_V_WIRE_DynamicMaskWorldNormal", transform.up);
            }
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
