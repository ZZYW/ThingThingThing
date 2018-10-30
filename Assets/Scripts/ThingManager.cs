using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class ThingManager : MonoBehaviour {
    public static ThingManager main;

    public GameObject chatBubblePrefab;
    public Transform chatBubbleCanvas;

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
                AddDigalogueBubble (newThing);
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

    void AddDigalogueBubble (GameObject target) {

        //if it has chatbubble(old one), destroy it and remember its position
        Transform existingChatBubble = target.transform.Find ("Chat Balloon");
        Vector3 bubblePosition = Vector3.up * target.GetComponent<Thing> ().settings.chatBubbleOffsetHeight; //default position
        if (existingChatBubble != null) {
            bubblePosition = existingChatBubble.transform.localPosition;
            Destroy (existingChatBubble.gameObject);
        }

        GameObject nChatBubble = Instantiate (chatBubblePrefab, Vector3.zero, Quaternion.identity);
        nChatBubble.transform.SetParent (chatBubbleCanvas, true);

        //let chatbubble and thing know who each other.p
        nChatBubble.GetComponent<SimpleChatBubble> ().host = target.transform;
        nChatBubble.GetComponent<SimpleChatBubble> ().offsetPos = bubblePosition;
        target.GetComponent<Thing> ().chatBubble = nChatBubble.GetComponent<SimpleChatBubble> ();
    }
}