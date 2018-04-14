using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTriggerEventSender : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Thing"))
        {
            Creature thing = other.gameObject.GetComponent<Creature>();
            thing.OnTouchWater();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Thing"))
        {
            Creature thing = other.gameObject.GetComponent<Creature>();
            thing.OnLeaveWater();
        }
    }



}
