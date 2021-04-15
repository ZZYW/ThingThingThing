using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class ThingManager : MonoBehaviour
{
    public static ThingManager main;

    // public Transform chatBubbleCanvas;

    public List<GameObject> AllThings;
    public GameObject coord;

    // public AudioMixer TTTAudioMixer;

    public bool generateThings;
    //public Transform spawnBox;

    [Range(20, 100)]
    public int spawnAreaRadius = 50;

    private void Awake()
    {
        main = this;

        //init all things
        Object[] allThingPrefabs = Resources.LoadAll("Things/") as Object[];

        if (generateThings)
        {
            //Spawn Things
            foreach (GameObject thing in allThingPrefabs)
            {
                GameObject newThing = Instantiate(thing, transform);
                newThing.transform.parent = transform;
                newThing.transform.position = GetSpawnPosition();
                var newCoord = GameObject.Instantiate(coord, newThing.transform.position, Quaternion.identity);
                newCoord.transform.SetParent(newThing.transform);
                newCoord.transform.localRotation = Quaternion.identity;
                newCoord.transform.localScale = Vector3.one / 20f;



                AllThings.Add(newThing);
            }
        }

        //Instantiate chat bubble objects
        ChatBubbleManager.main.Init(allThingPrefabs.Length);

    }

    public void MinusOneVertex()
    {
        // var allMesh = new 
    }


    public Vector3 GetSpawnPosition()
    {
        return new Vector3(Random.Range(-spawnAreaRadius, spawnAreaRadius), 20, Random.Range(-spawnAreaRadius, spawnAreaRadius));
    }
}