using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ThingManager : MonoBehaviour
{

    public static ThingManager main;
    public GameObject[] AllThings;

    public Transform spawnBox;


    NavMeshAgent agent;

    List<GameObject> nameLabels;


    private void Awake()
    {
        main = this;

        agent = new NavMeshAgent();
        nameLabels = new List<GameObject>();


        //init all things
        Object[] allThingPrefabs = Resources.LoadAll("Things/") as Object[];

        List<GameObject> allThings = new List<GameObject>();
        foreach (GameObject i in allThingPrefabs)
        {
            if (i.GetComponent<Creature>() != null)
            {
                allThings.Add(i);
            }
        }


        foreach (GameObject thing in allThings)
        {
            //agent.SetDestination(new Vector3(Random.Range(-100f, 100f), 10f, Random.Range(-100f, 100f)));
            //Vector3 randomLoc = agent.destination;
            //GameObject temp = new GameObject();
            //temp.transform.position = randomLoc;
            //test
            for (int i = 0; i < 14; i++)
            {
                GameObject newThing = Instantiate(thing, transform);
                newThing.transform.parent = transform;
            }

            //Destroy(temp);
        }

    }

    // Use this for initialization
    void Start()
    {
        AllThings = GameObject.FindGameObjectsWithTag("Thing");



        GameObject labelContainers = new GameObject("Label Containers");

        labelContainers.transform.parent = transform;

        //generate markers
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
