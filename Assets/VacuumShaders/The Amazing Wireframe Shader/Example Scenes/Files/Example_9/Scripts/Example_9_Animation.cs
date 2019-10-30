using UnityEngine;
using System.Collections;

public class Example_9_Animation : MonoBehaviour
{
    //Camera rotation target and speed
    public Transform rotateTarget;
    public float rotateSpeed = 1;


    //Dynamic GI actors
    public Renderer[] actors;
      
    //Used for color changing animation
    public Gradient gradient;


    //Time counter
    float deltaTime;

    //Currently animated GI actor index
    int index;

    //What animation are we currently playing
    bool playFlickeringAnimation;
    bool playColorChangingAnimation;
    bool playCutoutAnimation;


    // Use this for initialization
    void Start ()
    {
        //Make GI actor invisible
        for (int i = 0; i < actors.Length; i++)
        {
            ActivateActor(actors[i], 0);          
        }

        index = 0;


        playFlickeringAnimation = false;
        playColorChangingAnimation = false;
        playCutoutAnimation = false;


        //Update Unity DynamicGI every frame
        DynamicGI.updateThreshold = -1;


        StartCoroutine("BeginFlickeringAnimation");
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Rotate camera around target
        transform.RotateAround(rotateTarget.position, Vector3.up, Time.deltaTime * rotateSpeed);


        if(playFlickeringAnimation)
            PlayFlickering();

        if (playColorChangingAnimation)
            PlayColorChanging();

        if (playCutoutAnimation)
            PlayCutout();
    }

    void LateUpdate()
    {
        transform.LookAt(rotateTarget);
    }


    IEnumerator BeginFlickeringAnimation()
    {
        yield return new WaitForSeconds(4);

        index = 0;
        deltaTime = 0;
        playFlickeringAnimation = true;
    }

    IEnumerator BeginColorChangingAnimation()
    {
        yield return new WaitForSeconds(3);

        deltaTime = 0;
        playColorChangingAnimation = true;
    }

    IEnumerator BeginCutOutAnimation()
    {
        yield return new WaitForSeconds(1);

        index = 0;
        deltaTime = 0;
        playCutoutAnimation = true;
    }


    void PlayFlickering()
    {
        //Light flickering
        float intensity = Random.Range(-0.05f, 1f);


        //Update GI actors
        ActivateActor(actors[index], intensity);



        //Increase delta time
        deltaTime += Time.deltaTime;

        //
        if (deltaTime > 2)
        {
            //Reset delta to make it useable for another animation cicle
            deltaTime = 0;


            //Make current GI actor fully visible
            ActivateActor(actors[index], 1);

            //and swith to another one
            index += 1;

            //if it is possible
            if (index >= actors.Length)
            {
                playFlickeringAnimation = false;

                StartCoroutine("BeginColorChangingAnimation");
            }
        }
    }

    void ActivateActor(Renderer _renderer, float _intensity)
    {
        //Make flikering effect
        _renderer.gameObject.SetActive(_intensity > 0);


        //Enable or Disable GI inside material
        _renderer.material.SetFloat("_V_WIRE_DynamicGI", _intensity > 0 ? 1 : 0);
        

        //Notify Unity GI about material data changing
        RendererExtensions.UpdateGIMaterials(_renderer);
        DynamicGI.UpdateEnvironment();
    }
          

    void PlayColorChanging()
    {
        deltaTime += Time.deltaTime * 0.13f;

        Color color = gradient.Evaluate(deltaTime);


        for (int i = 0; i < actors.Length; i++)
        {
            actors[i].material.SetColor("_V_WIRE_Color", color);

            RendererExtensions.UpdateGIMaterials(actors[i]);
            DynamicGI.UpdateEnvironment();
        }


        if (deltaTime > 1)
        {
            playColorChangingAnimation = false;

            StartCoroutine("BeginCutOutAnimation");
        }
    }


    void PlayCutout()
    {
        deltaTime += Time.deltaTime * 0.175f;


        float alphaValue = Mathf.Lerp(1f, -1f, Mathf.Clamp01(deltaTime));


        for (int i = 0; i < actors.Length; i++)
        {
            actors[i].material.SetFloat("_V_WIRE_TransparentTex_Alpha_Offset", alphaValue);

            //Notify Unity GI about material data changing
            RendererExtensions.UpdateGIMaterials(actors[i]);
            DynamicGI.UpdateEnvironment();
        }
       


        if(deltaTime > 1)
        {
            playCutoutAnimation = false;

            //The End
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
