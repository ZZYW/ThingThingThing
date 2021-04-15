using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFollowingCam : MonoBehaviour
{
    public Transform followTarget;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (followTarget != null)
        {

            transform.position = followTarget.position + Vector3.forward * 20 + Vector3.up * 5;
            transform.LookAt(followTarget);
        }
    }
}
