using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingMotor : MonoBehaviour {

    Vector3 target;
    float acceleration;
    bool seekingTarget = true;
    float rotationSmoothSpeed = 3.14f / 2f;
    bool facingTarget = true;

    [HideInInspector]
    public Rigidbody rb;

    public void SetTarget (Vector3 target) {
        this.target = target;
    }

    public void SetAccel (float value) {
        acceleration = value;
    }

    private void Awake () {
        rb = GetComponent<Rigidbody> ();
    }

    void Start () {
        rb.angularDrag = 10f;
    }

    void FixedUpdate () {
        Vector3 force = Vector3.zero;
        if (seekingTarget) {
            Vector3 diff = target - transform.position;
            if (diff.magnitude < 5) return;
            diff.Normalize ();
            diff *= acceleration;
            force = diff;

            if (facingTarget) {
                Quaternion targetRotation = Quaternion.LookRotation (diff);
                Quaternion newRotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.fixedDeltaTime * rotationSmoothSpeed);
                rb.rotation = newRotation;
            }

        }

        rb.AddForce (force);

    }
    public void FacingTarget (bool value) {
        facingTarget = value;
    }

    public void Stop () {
        rb.velocity = Vector3.zero;
    }
}