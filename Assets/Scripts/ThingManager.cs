using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class ThingManager : MonoBehaviour {
    public static ThingManager main;

    
    public List<GameObject> AllThings;

    public bool generateThings;
    //public Transform spawnBox;

    int spawnAreaRadius = 40;

    private void Awake () {
        main = this;

        //init all things
        Object[] allThingPrefabs = Resources.LoadAll ("Things/") as Object[];

        if (generateThings) {
            //Spawn Things
            foreach (GameObject thing in allThingPrefabs) {
                GameObject newThing = Instantiate (thing, transform);
                newThing.transform.parent = transform;
                newThing.transform.position = new Vector3 (Random.Range (-spawnAreaRadius, spawnAreaRadius), 0, Random.Range (-spawnAreaRadius, spawnAreaRadius));
                AllThings.Add (newThing);
            }
        }

    }

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }
}