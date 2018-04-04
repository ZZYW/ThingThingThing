using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class Creature : MonoBehaviour
{
    //pre filled 
    public GameObject ChatBalloonPrefab;

    //PRIVATE
    NavMeshAgent nmAgent;
    SphereCollider neighborDetector;
    List<GameObject> neighborList;
    ChatBalloon chatBalloon;



    //PUBLIC

    public int neighborDetectorRadius = 30;
    public int chatBalloonYOffset = 10;

    //PROPERTIES

    //Time
    static System.DateTime CurrentTime
    {
        get
        {
            return TOD_Data.main.CurrentDatetime;
        }
    }

    int NeighborCount
    {
        get {
            return neighborList.Count;
        }
    }



    //MONOBEHAVIOR METHODS
    private void Start()
    {
        //tag
        gameObject.tag = "Thing";

        //neighbor detector
        neighborDetector = GetComponent<SphereCollider>();
        neighborDetector.isTrigger = true;
        neighborDetector.radius = neighborDetectorRadius;

        //nav mesh agent

        nmAgent = GetComponent<NavMeshAgent>();
        Debug.Log(nmAgent);

        //Instantiating ChatBallon Object
        GameObject chatBalloonGameobject = Instantiate(ChatBalloonPrefab, gameObject.transform);
        chatBalloonGameobject.transform.position += new Vector3(0, chatBalloonYOffset, 0);
        chatBalloon = chatBalloonGameobject.GetComponent<ChatBalloon>();

        //Init
        neighborList = new List<GameObject>();
    }

    private void Update()
    {
        //Mouse left key
        if(Input.GetMouseButtonUp(0))
        {
            nmAgent.SetDestination(RandomVec3(-40,40));
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log(NeighborCount);
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            Speak("Hello World", 1f);
        }
    }



    //TTT BEHAVIOURS

    //Behaviours
    protected void SetTarget(Vector3 target)
    {
        nmAgent.SetDestination(target);
    }

    protected void RotateSelf(Vector3 angle)
    {
        transform.Rotate(angle);
    }

    protected void SetScale(Vector3 newScale)
    {
        transform.localScale = newScale;
    }

    protected void Speak(string content, float howLong)
    {
        chatBalloon.SetTextAndActive(content, howLong);
    }

    protected void Spark(string ParticleType)
    {

    }

    protected void PlaySound(string soundName)
    {

    }

    protected void PlayAnimation(string animationName)
    {

    }


    //Events
    void OnMeetSomeone(GameObject other)
    {
        //when you meet another Thing
        //do something
        Speak("I met " + other.name, 2f);
   
    }

    void OnLeaveSomeone(GameObject other)
    {
        //when another Thing leaves you
        //do something
        Speak("I am leaving from " + other.name, 2f);
    }


    //=---

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Thing")
        {
            OnMeetSomeone(other.gameObject);
            //if neighborList doesn't contain this object, then add it into the list
            if(!neighborList.Contains(other.gameObject))
            {
                neighborList.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Thing")
        {
            OnLeaveSomeone(other.gameObject);
            //if neighborList contains this object, then remove it from the list
            if(neighborList.Contains(other.gameObject))
            {
                neighborList.Remove(other.gameObject);
            }
        }
    }







    //others

    public static Vector3 RandomVec3(float a, float b)
    {
        return new Vector3(Random.Range(a, b), Random.Range(a, b), Random.Range(a, b));
    }





}
