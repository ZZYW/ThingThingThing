using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlableCameraController : MonoBehaviour {

    [Range (0, 5)]
    public float speedFactor = 0.1f;
    [Range (1, 10)]
    public float jumpSpeed = 8.0f;
    [Range (1, 20)]
    public float gravity = 20.0f;

    // public CharacterController characterController;

    private Vector3 motion;
    private Vector3 rot;

    //
    Vector2 leftAxis;
    Vector2 rightAxis;
    

    

    // Use this for initialization
    void Start () {
        // characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update () {

        leftAxis.x = Input.GetAxis("JoyStickLeftX");
        leftAxis.y = Input.GetAxis("JoyStickLeftY");
        rightAxis.x = Input.GetAxis("JoyStickRightX");
        rightAxis.y = Input.GetAxis("JoyStickRightY");
        

        motion *= speedFactor * Time.deltaTime;

        // if (Input.GetButton ("Jump")) {
        //     motion.y = jumpSpeed;
        // }

        // motion.y -= gravity * Time.deltaTime;

        transform.Translate(motion, Space.Self);


        // characterController.Move(motion * Time.deltaTime);

    }

}