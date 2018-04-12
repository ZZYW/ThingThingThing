using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class ThingMotor : MonoBehaviour
{

    public Vector3 Target { get; set; }
    public float MaxSpeed { get; set; }

    float speed;
    bool seekingTarget = true;
    float rotationSmoothSpeed;

    Rigidbody rb;


    private void Awake()
    {
        //default value
        MaxSpeed = 4;
        speed = 2;
        rotationSmoothSpeed = 3.14f / 3f;
    }


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.freezeRotation = true;
    }


    void FixedUpdate()
    {

        Vector3 force = Vector3.zero;

        if (seekingTarget)
        {
            Vector3 diff = Target - transform.position;
            diff.Normalize();
            diff *= speed;
            force += diff;

            Debug.DrawLine(transform.position, Target);


            Quaternion targetRotation = Quaternion.LookRotation(diff);
            Quaternion newRotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmoothSpeed);
            rb.rotation = newRotation;



        }

        //Quaternion deltaRotation = Quaternion.Euler(EulerAngleVelocity * Time.fixedDeltaTime);
        //rb.MoveRotation(rb.rotation * deltaRotation);

        //Vector3 deltaRot = Target - transform.forward;

        if (rb.velocity.magnitude < MaxSpeed)
        {
            rb.AddForce(force + Vector3.up/3) ;
        }



    }
}
