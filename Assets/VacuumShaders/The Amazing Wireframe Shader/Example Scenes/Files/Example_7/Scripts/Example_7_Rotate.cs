using UnityEngine;
using System.Collections;

public class Example_7_Rotate : MonoBehaviour {

    public float speed = 1;

    public float time;


    float fullRotate = 0;

    public AnimationCurve cuve;

    Material mat;
	// Use this for initialization
	void Start () {

        time = 0;

        fullRotate = 360f / speed;

        if(GetComponent<Renderer>() != null)
            mat = GetComponent<Renderer>().sharedMaterial;
    }
	
	// Update is called once per frame
	void Update () {

        if (mat != null)
        {
            time += Time.deltaTime / fullRotate;


            float cValue = cuve.Evaluate(time);

            mat.SetFloat("_V_WIRE_Tessellation", 1 + cValue * cValue * 31);
            mat.SetFloat("_V_WIRE_Tessellation_DispStrength", Mathf.Clamp01(cValue * 1.0f) * 3f);
        }

        transform.Rotate(Vector3.up, Time.deltaTime * speed);
	}
}
