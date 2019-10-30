using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using VacuumShaders.TheAmazingWireframeShader;

public class Example_6_Runtime : MonoBehaviour 
{
    //////////////////////////////////////////////////////////////////////////////
    //                                                                          // 
    //Variables                                                                 //                
    //                                                                          //               
    ////////////////////////////////////////////////////////////////////////////// 
    public Text uiText;

    Example_5_MCBlob mcBlob;

    MeshFilter meshFilter;
    //////////////////////////////////////////////////////////////////////////////
    //                                                                          // 
    //Unity Functions                                                           //                
    //                                                                          //               
    //////////////////////////////////////////////////////////////////////////////
	void Start () 
    {
        //
        meshFilter = GetComponent<MeshFilter>();

        //
        mcBlob = new Example_5_MCBlob(meshFilter);

	}

    void Update()
    {
        mcBlob.Update();

              

        //        
        uiText.text = "GeometryShader does not need mesh with barycentric coordinates. Generating speed: Instantly";
        uiText.text += "\nVertexCount: " + mcBlob.finalMesh.vertexCount + " (same as original)";
        uiText.text += "\nTrinaglesCount: " + (mcBlob.finalMesh.triangles.Length / 3);


        
        //render new mesh
        meshFilter.sharedMesh = mcBlob.finalMesh;
    }
}
