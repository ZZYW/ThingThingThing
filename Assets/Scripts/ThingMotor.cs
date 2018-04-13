using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class ThingMotor : MonoBehaviour
{

    Vector3 target;
    float acceleration = 2;
    bool seekingTarget = true;
    float rotationSmoothSpeed = 3.14f / 3f;

    [HideInInspector]
    public Rigidbody rb;


    public void SetTarget(Vector3 target)
    {
        this.target = target;
    }

    public void SetAccel(float value)
    {
        acceleration = value;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Start()
    {
        //rb.freezeRotation = true;
        rb.angularDrag = 10f;
    }


    void FixedUpdate()
    {
        Vector3 force = Vector3.zero;
        if (seekingTarget)
        {
            Vector3 diff = target - transform.position;
            if (diff.magnitude < 5) return;
            diff.Normalize();
            diff *= acceleration;
            force += diff;

            Quaternion targetRotation = Quaternion.LookRotation(diff);
            Quaternion newRotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmoothSpeed);
            rb.rotation = newRotation;
        }

        rb.AddForce(force);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, target);
        Gizmos.DrawSphere(target, 1f);
    }
}
