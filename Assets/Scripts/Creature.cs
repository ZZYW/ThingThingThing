using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]

public class Creature : MonoBehaviour
{

    //---------------------------------------------------------------------------------
    //  PROPERTIES AND FIELDS
    //---------------------------------------------------------------------------------


    //  PREFABS WE WILL NEED TO FILL IN BEFOREHAND
    public GameObject ChatBalloonPrefab;
    public GameObject ParticleExplode;



    //Variables for us to tweak on Unity GUI
    public int neighborDetectorRadius = 30;
    public int chatBalloonYOffset = 10;


    private NavMeshAgent nmAgent;
    private SphereCollider neighborDetector;
    private ChatBalloon chatBalloon;
    private ParticleSystem explodePS;
    private AudioSource audioSource;


    private List<GameObject> neighborList;
    private string soundFilePath = "Sounds/";


    private System.DateTime CurrentTime
    {
        get
        {
            return TOD_Data.main.CurrentDatetime;
        }
    }

    private int NeighborCount
    {
        get
        {
            return neighborList.Count;
        }
    }




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


        //ChatBallon
        GameObject chatBalloonGameobject = Instantiate(ChatBalloonPrefab, gameObject.transform);
        chatBalloonGameobject.transform.position += new Vector3(0, chatBalloonYOffset, 0);
        chatBalloon = chatBalloonGameobject.GetComponent<ChatBalloon>();

        //Instantiating Particle Object
        //TODO: add particle system prefab to each models
        explodePS = GetComponentInChildren<ParticleSystem>();

        //Sound
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.spatialBlend = 0.9f;
        audioSource.maxDistance = 35;



        //Init
        neighborList = new List<GameObject>();

    }

    private void Update()
    {
        //Mouse left key
        if (Input.GetMouseButtonUp(0))
        {
            nmAgent.SetDestination(RandomVec3(-40, 40));
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log(NeighborCount);

        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            //Speak("Hello World", 1f);
            Spark(new Color(1, 1, 1), 20);
            PlaySound("cartoon-pinch");
        }

    }

    //---------------------------------------------------------------------------------
    //  BEHAVIOURS
    //---------------------------------------------------------------------------------


    private void SetTarget(Vector3 target)
    {
        nmAgent.SetDestination(target);
    }

    private void RotateSelf(Vector3 angle)
    {
        transform.Rotate(angle);
    }

    private void SetScale(Vector3 newScale)
    {
        transform.localScale = newScale;
    }

    private void Speak(string content, float stayLength)
    {
        chatBalloon.SetTextAndActive(content, stayLength);
    }

    private void Spark(Color particleColor, int numberOfParticles)
    {
        var particleMain = explodePS.main;
        particleMain.startColor = particleColor;

        var newBurst = new ParticleSystem.Burst(0f, numberOfParticles);
        explodePS.emission.SetBurst(0, newBurst);
        explodePS.Play();
    }

    private void PlaySound(string soundName)
    {
        audioSource.clip = Resources.Load(soundFilePath + soundName) as AudioClip;
        audioSource.Play();
    }

    private void PlayAnimation(string animationName)
    {

    }


    //---------------------------------------------------------------------------------
    //  EVENTS
    //---------------------------------------------------------------------------------



    private void OnMeetSomeone(GameObject other)
    {
        //when you meet another Thing
        //do something
        Speak("I met " + other.name, 2f);


    }

    private void OnLeaveSomeone(GameObject other)
    {
        //when another Thing leaves you
        //do something
        Speak("I am leaving from " + other.name, 2f);
    }




    //---------------------------------------------------------------------------------
    //  OTHER 
    //---------------------------------------------------------------------------------


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Thing")
        {
            OnMeetSomeone(other.gameObject);
            //if neighborList doesn't contain this object, then add it into the list
            if (!neighborList.Contains(other.gameObject))
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
            if (neighborList.Contains(other.gameObject))
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
