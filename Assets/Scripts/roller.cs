using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roller : MonoBehaviour
{

    Rigidbody rb;
    public Vector3 torque;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();


        StartCoroutine(AddForce());
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    IEnumerator AddForce()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            rb.AddTorque(torque, ForceMode.Impulse);
        }
    }
}
