using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameLabel : MonoBehaviour
{
    public Transform target;
    public float yoffset;
    bool inited = false;

    Vector3 rotateAxis;


    // Use this for initialization
    void Start()
    {

        Renderer rend = GetComponent<Renderer>();
        Shader shader = Shader.Find("Custom/rainbow");
        rend.material.shader = shader;

        //set shader
        rend.material.SetInt("_Rainbow", 1);
        rend.material.SetFloat("_TimeOffset", Random.Range(0.0f, 10.0f));
        rend.material.SetFloat("_TimeScale", Random.Range(10f, 100f));
        rend.material.SetColor("_RainbowColor1", new Color32(249, 54, 54, 255));
        rend.material.SetColor("_RainbowColor2", Color.green);
        rend.material.SetColor("_RainbowColor3", new Color32(0, 29, 255, 255));


        transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);

        //check after 3s, if this is not inited, then destroy
        Invoke("Check", 3f);

        rotateAxis = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    public void Init(Transform target, float yoffset)
    {
        this.target = target;
        this.yoffset = yoffset;
        inited = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (inited)
        {
            Vector3 targetPos = target.transform.position;
            targetPos.y += yoffset;
            transform.position = targetPos;
            transform.Rotate(rotateAxis, 5);

        }

    }

    void Check()
    {
        if (!inited)
        {
            Destroy(gameObject);
        }
    }
}
