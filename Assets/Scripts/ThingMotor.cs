using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class ThingMotor : MonoBehaviour
{


    public Transform target;

    public float speed = 0.1f;


    Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        Vector3 force = Vector3.zero;

        if (target != null)
        {
            Vector3 diff = target.transform.position - transform.position;
            diff.Normalize();
            diff *= speed;
            force += diff;
        }


        rb.AddForce(force);


    }
}
