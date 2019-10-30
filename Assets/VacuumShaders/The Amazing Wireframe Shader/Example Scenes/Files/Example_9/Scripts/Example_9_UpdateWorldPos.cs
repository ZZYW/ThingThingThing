//Dynamic GI uses META pass, which does not have info about mesh position in the world.
//This script supportes shader with required mesh position info.
//World position info is required by DISTANCE FADE and DYNAMIC MASK effects.

using UnityEngine;

public class Example_9_UpdateWorldPos : MonoBehaviour
{
    Renderer mRenderer;
    Material mat;


	// Use this for initialization
	void Start ()
    {
        mRenderer = GetComponent<Renderer>();

        if(mRenderer != null)
            mat = mRenderer.material;


        //If mesh does not move, its enough to update shader only once
        if (mat != null && mRenderer != null)
        {
            mat.SetVector("_V_WIRE_ObjectWorldPos", transform.position);

            RendererExtensions.UpdateGIMaterials(mRenderer);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        //if(mat != null && mRenderer != null)
        //  mat.SetVector("_V_WIRE_ObjectWorldPos", transform.position);


        //As in current example GI depends on Distamce Fade effect
        //we need to update Unity GI on every frame

        RendererExtensions.UpdateGIMaterials(mRenderer);
    }
}
