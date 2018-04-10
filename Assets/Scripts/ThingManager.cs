using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class ThingManager : MonoBehaviour
{
    public static ThingManager main;
    public GameObject[] AllThings;

    //public Transform spawnBox;

    int spawnAreaRadius = 40;

    private void Awake()
    {
        main = this;

        //init all things
        Object[] allThingPrefabs = Resources.LoadAll("Things/") as Object[];
        var allThings = from t in allThingPrefabs
                        where ((GameObject)t).GetComponent<Creature>() != null
                        select t;

        //Spawn Things
        foreach (GameObject thing in allThings)
        {
            //for (int i = 0; i < 15; i++)
            //{
                GameObject newThing = Instantiate(thing, transform);
                newThing.transform.parent = transform;
                newThing.transform.position = new Vector3(Random.Range(-spawnAreaRadius, spawnAreaRadius), 0, Random.Range(-spawnAreaRadius, spawnAreaRadius));
            //}
        }

    }

    // Use this for initialization
    void Start()
    {
        AllThings = GameObject.FindGameObjectsWithTag("Thing");



        //generate markers
        GameObject labelContainers = new GameObject("Label Containers");
        labelContainers.transform.parent = transform;
        foreach (GameObject thing in AllThings)
        {
            GameObject labelCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            NameLabel nameLabel = labelCube.AddComponent<NameLabel>();
            labelCube.transform.parent = labelContainers.transform;
            nameLabel.Init(thing.transform, 2);
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}
