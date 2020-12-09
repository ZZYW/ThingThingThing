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
                // var newCoord = GameObject.Instantiate(coord, newThing.transform.position, Quaternion.identity);
                // newCoord.transform.SetParent(newThing.transform);
                // newCoord.transform.localRotation = Quaternion.identity;
                // newCoord.transform.localScale = Vector3.one;


                GetBubblePosition(newThing); //for old THINGs
                AllThings.Add(newThing);
            }
        }

        //Instantiate chat bubble objects
        ChatBubbleManager.main.Init(allThingPrefabs.Length);

    }

    void GetBubblePosition(GameObject target)
    {

        //if it has chatbubble(old one), destroy it and remember its position
        Transform existingChatBubble = target.transform.Find("Chat Balloon");

        //default position
        Vector3 bubblePosition = Vector3.up * target.GetComponent<Thing>().settings.chatBubbleOffsetHeight;

        if (existingChatBubble != null)
        {
            bubblePosition = existingChatBubble.transform.localPosition * target.transform.localScale.y;
            Destroy(existingChatBubble.gameObject);
        }
        target.GetComponent<Thing>().bubbleOffsetPosition = bubblePosition;
    }

    public Vector3 GetSpawnPosition()
    {
        return new Vector3(Random.Range(-spawnAreaRadius, spawnAreaRadius), 20, Random.Range(-spawnAreaRadius, spawnAreaRadius));
    }
}