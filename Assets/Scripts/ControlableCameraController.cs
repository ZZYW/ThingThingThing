using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlableCameraController : MonoBehaviour {

    public enum ControlMode {
        JOYSTICKS,
        KEYBOARD_MOUSE
    }

    public ControlMode controlMode;

    [Range (0, 50)]
    public float speedFactor = 0.1f;

    [Range (0, 100)]
    public float rotateSpeedFactor = 80;

    public Transform resetPoint;

    Vector2 leftAxis;
    Vector2 rightAxis;

    // Rigidbody rb;
    CharacterController controller;
    private float unusedStartTimeStamp;
    [Range (1, 100)] public float timeTillResetPosition;
    private bool idle = true;

    void Awake () {
        if (Display.displays.Length < 2 && !Application.isEditor) {
            Debug.LogWarning ("destroying controlableCameraController");
            Destroy (gameObject);
        }
    }
    // Use this for initialization
    void Start () {
        // rb = GetComponent<Rigidbody> ();
        controller = GetComponent<CharacterController> ();
    }

    // Update is called once per frame
    void Update () {

        if (controlMode == ControlMode.JOYSTICKS) {
            leftAxis.x = Input.GetAxis ("JoyStickLeftX");
            leftAxis.y = Input.GetAxis ("JoyStickLeftY");
            rightAxis.x = Input.GetAxis ("JoyStickRightX");
            rightAxis.y = Input.GetAxis ("JoyStickRightY");
        } else if (controlMode == ControlMode.KEYBOARD_MOUSE) {
            leftAxis.x = Input.GetAxis ("Horizontal");
            leftAxis.y = Input.GetAxis ("Vertical");
            rightAxis.x = Input.GetAxis ("Mouse X");
            rightAxis.y = Input.GetAxis ("Mouse Y");
        }

        // if (leftAxis.x < 0.001f && leftAxis.y < 0.001f && rightAxis.x < 0.001f && rightAxis.y < 0.001f && !idle) {
        //     unusedStartTimeStamp = Time.time;
        //     idle = true;
        //     Debug.Log ("idle start");
        //     ThingConsole.LogWarning ("controller goes into idle");

        // }

        // if (leftAxis.x > 0.001f || leftAxis.y > 0.001f || rightAxis.x > 0.001f || rightAxis.y > 0.001f) {
        //     idle = false;
        //     ThingConsole.LogWarning ("controller being picked up.");
        // }

        // if (Time.time - unusedStartTimeStamp > timeTillResetPosition && idle) {
        //     ResetPosition ();
        // }

        leftAxis *= speedFactor * Time.deltaTime;
        rightAxis *= rotateSpeedFactor * Time.deltaTime;

        Vector3 translation = new Vector3 (leftAxis.x, 0, leftAxis.y);
        translation = transform.TransformVector (translation);
        controller.Move (translation);

        // rb.MovePosition (transform.position + translation);
        transform.Rotate (Vector3.up, rightAxis.x);
        transform.Rotate (Vector3.left, rightAxis.y);

    }

    private void ResetPosition () {
        transform.position = resetPoint.position;
        transform.rotation = Quaternion.identity;
    }
}