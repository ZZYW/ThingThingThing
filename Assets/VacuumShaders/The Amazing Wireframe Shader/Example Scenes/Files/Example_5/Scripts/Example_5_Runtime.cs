using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using VacuumShaders.TheAmazingWireframeShader;

public class Example_5_Runtime : MonoBehaviour 
{
    //////////////////////////////////////////////////////////////////////////////
    //                                                                          // 
    //Variables                                                                 //                
    //                                                                          //               
    ////////////////////////////////////////////////////////////////////////////// 
    public Text uiText;
    public Toggle uiToggle;

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

        //
        uiToggle.gameObject.SetActive(true);
	}

    void Update()
    {
        mcBlob.Update();


        float time = Time.realtimeSinceStartup;
        Mesh newWireMesh = null;

        if (uiToggle.isOn)
            newWireMesh = WireframeGenerator.Generate(mcBlob.finalMesh);
        else
            newWireMesh = WireframeGenerator.GenerateFast(mcBlob.finalMesh);

        //        
        uiText.text = "Wireframe generation speed: " + (Time.realtimeSinceStartup - time).ToString("f5") + " ms";
        uiText.text += "\nVertexCount: " + newWireMesh.vertexCount;
        uiText.text += "\nTrinaglesCount: " + (newWireMesh.triangles.Length / 3);




        //Destroy previously generated wirefame mesh
        if (meshFilter.sharedMesh != null)
            DestroyImmediate(meshFilter.sharedMesh);

        //render new wireframe mesh
        meshFilter.sharedMesh = newWireMesh;
    }
}
