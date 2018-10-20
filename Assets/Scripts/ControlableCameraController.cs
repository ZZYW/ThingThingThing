using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlableCameraController : MonoBehaviour {

    [Range (0, 50)]
    public float speedFactor = 0.1f;

    [Range(0, 100)]
    public float rotateSpeedFactor = 80;

    [Range (1, 10)]
    public float jumpSpeed = 8.0f;
    [Range (1, 20)]
    public float gravity = 20.0f;

    
    private Vector2 leftAxis;
    private Vector2 rightAxis;
    

    

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


        Debug.Log("left axis: " + leftAxis + "    right axis:  " + rightAxis);
        

        leftAxis *= speedFactor * Time.deltaTime;
        rightAxis *= rotateSpeedFactor * Time.deltaTime;

        

        transform.Translate(new Vector3(leftAxis.x, 0, leftAxis.y), Space.Self);
        transform.Rotate(Vector3.up, rightAxis.x);
        transform.Rotate(Vector3.left, rightAxis.y);



        // characterController.Move(motion * Time.deltaTime);

    }

}