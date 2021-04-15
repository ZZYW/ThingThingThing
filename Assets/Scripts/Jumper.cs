using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    Rigidbody rb;
    public float mul;
    Vector3 force;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();


        StartCoroutine(AddForce());
    }

    // Update is called once per frame
    void Update()
    {
        force = transform.forward + transform.up;
    }


    IEnumerator AddForce()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            rb.AddForce(force * mul, ForceMode.Impulse);
        }
    }
}
