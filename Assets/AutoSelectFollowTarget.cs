using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSelectFollowTarget : MonoBehaviour
{

    public int changeInternal = 30;
    CameraFollowThing cameraFollowThing;

    // Start is called before the first frame update
    void Start()
    {
        cameraFollowThing = GetComponent<CameraFollowThing>();
        InvokeRepeating("GetNewTarget", 0, changeInternal);
    }

    void GetNewTarget()
    {
        var allThings = ThingManager.main.AllThings;
        GameObject oneRandomThing = allThings[(int)Random.Range(0, allThings.Count)];
        cameraFollowThing.followTarget = oneRandomThing.transform;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
