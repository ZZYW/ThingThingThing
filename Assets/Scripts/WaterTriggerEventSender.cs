using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTriggerEventSender : MonoBehaviour {
    private void OnTriggerEnter (Collider other) {
        if (other.gameObject.CompareTag ("Thing")) {
            Thing thing = other.gameObject.GetComponent<Thing> ();
            thing.OnWaterEnter ();
        }
    }

    private void OnTriggerExit (Collider other) {
        if (other.gameObject.CompareTag ("Thing")) {
            Thing thing = other.gameObject.GetComponent<Thing> ();
            thing.OnWaterExit ();
        }
    }

}