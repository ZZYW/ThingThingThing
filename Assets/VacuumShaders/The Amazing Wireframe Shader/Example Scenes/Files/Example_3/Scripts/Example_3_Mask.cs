using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Example_3_Mask : MonoBehaviour 
{
    //////////////////////////////////////////////////////////////////////////////
    //                                                                          // 
    //Variables                                                                 //                
    //                                                                          //               
    //////////////////////////////////////////////////////////////////////////////
    public Color wireColor = Color.black;
    public float wireSize = 1;
    [Range(1f, 10f)]
    public float radius = 1;

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
            
            //Enable 'Sphere Mask' keyword
            sceneMaterials[i].DisableKeyword("V_WIRE_DYNAMIC_MASK_OFF");
            sceneMaterials[i].DisableKeyword("V_WIRE_DYNAMI_MASK_PLANE");
            sceneMaterials[i].DisableKeyword("V_WIRE_DYNAMIC_MASK_BOX");
            sceneMaterials[i].EnableKeyword("V_WIRE_DYNAMIC_MASK_SPHERE");

            //Set Wire color
            sceneMaterials[i].SetColor("_V_WIRE_Color", wireColor);
            sceneMaterials[i].SetFloat("_V_WIRE_Size", wireSize < 0 ? 0 : wireSize);

            //Update 'Sphere Mask' shader parameters
            sceneMaterials[i].SetVector("_V_WIRE_DynamicMaskWorldPos", transform.position);
            sceneMaterials[i].SetVector("_V_WIRE_DynamicMaskWorldNormal", transform.up);
            sceneMaterials[i].SetFloat("_V_WIRE_DynamicMaskRadius", radius);
        }

        if(Application.isPlaying)
            radius = Mathf.PingPong(Time.realtimeSinceStartup * 2, 10f);
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
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
