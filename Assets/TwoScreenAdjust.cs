using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoScreenAdjust : MonoBehaviour
{

    public Camera leftCam;
    public Camera rightCam;

    // Start is called before the first frame update
    void Start()
    {
        //unify size
        float size = leftCam.orthographicSize;
        rightCam.orthographicSize = size;
        //calculate offset
        float offsetValue = leftCam.aspect * leftCam.orthographicSize;

        var leftCamLoPos = leftCam.transform.localPosition;
        leftCamLoPos.x = -offsetValue;
        leftCam.transform.localPosition = leftCamLoPos;

        var rightCamLoPos = rightCam.transform.localPosition;
        rightCamLoPos.x = offsetValue;
        rightCam.transform.localPosition = rightCamLoPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
