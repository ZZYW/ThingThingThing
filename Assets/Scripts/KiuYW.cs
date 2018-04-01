using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class KiuYW : Creature {


    private void Start()
    {
    //    SetTarget(RandomVec3(-100,100));
        transform.Rotate(RandomVec3(-90,90));
    }

    private void Update()
    {
        //for testing
        if(Input.GetMouseButton(0))
        {
            //SetTarget(RandomVec3(-100, 100));
            transform.Rotate(new Vector3(0,1,0));
        }


        if (Input.GetMouseButton(1))
        {
            SetTarget(RandomVec3(-100, 100));
            //transform.Rotate(new Vector3(0, 1, 0));
        }

    }




}
