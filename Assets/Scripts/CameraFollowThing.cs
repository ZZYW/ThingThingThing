using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowThing : MonoBehaviour
{

    public Transform followTarget;
    public float followDistance;
    public float elevation;
    public UnityEngine.UI.Text HUDText;
    public bool rollCalling = true;
    public float interval = 5f;

    // Use this for initialization
    void Start()
    {
        // StartCoroutine(ChangeFollowTarget());
    }

    // Update is called once per frame
    void Update()
    {
        if (followTarget == null) return;
                        
        transform.position = followTarget.position - (followDistance * followTarget.forward) + new Vector3(0, elevation, 0);

        transform.LookAt(followTarget);
        HUDText.text = "Viewing:  " + followTarget.GetComponent<Thing>().MyName;
    }

    IEnumerator ChangeFollowTarget()
    {
        while (rollCalling)
        {
            followTarget = RandomThing().transform;
            yield return new WaitForSeconds(interval);
        }

    }


    GameObject RandomThing()
    {
        int len = ThingManager.main.AllThings.Count;
        return ThingManager.main.AllThings[Random.Range(0, len)];
    }

}