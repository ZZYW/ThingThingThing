using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TOD_Data : MonoBehaviour
{

    public static TOD_Data main;

    public System.DateTime CurrentDatetime
    {
        get
        {
            return GetComponent<TOD_Sky>().Cycle.DateTime;
        }
    }

    private void Awake()
    {
        if (main == null)
        {
            main = this;
        }

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
