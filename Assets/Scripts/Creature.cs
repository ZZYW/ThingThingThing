using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class Creature : MonoBehaviour
{

    //---------------------------------------------------------------------------------
    //  PROPERTIES AND FIELDS
    //---------------------------------------------------------------------------------

    //PUBLIC
    //[Header("Need to be filled to work properly")]
    //public GameObject ChatBalloonPrefab;
    //public GameObject ParticlePrefab; 


    [Header("Tweak-able")]
    [Tooltip("Distance from camera to object center on 3rd personn camera following mode")]
    public int desiredFollowDistance = 3;
    [Tooltip("how large is my neighbor awareness radar")]
    public int neighborDetectorRadius = 10;


    //PRIVATE
    private NavMeshAgent nmAgent;
    private SphereCollider neighborDetector;
    private ChatBalloon chatBalloon;
    private ParticleSystem explodePS;
    private AudioSource audioSource;

    private List<GameObject> neighborList;
    private string soundFilePath = "Sounds/";



    //is it daytime?
    //TOD_Data.main.IsDay;
    //is it night time?
    //TOD_Data.main.IsNight;



    private int NeighborCount
    {
        get
        {
            return neighborList.Count;
        }
    }

    private void OnEnable()
    {
        TTTEventsManager.OnSomeoneSpeaking += OnSomeoneSpeaking;
        TTTEventsManager.OnSomeoneSparking += OnSomeoneSparking;
        TOD_Data.OnSunset += OnSunset;
        TOD_Data.OnSunrise += OnSunrise;
    }

    private void OnDisable()
    {
        TTTEventsManager.OnSomeoneSpeaking -= OnSomeoneSpeaking;
        TTTEventsManager.OnSomeoneSparking -= OnSomeoneSparking;
        TOD_Data.OnSunset -= OnSunset;
        TOD_Data.OnSunrise -= OnSunrise;
    }


    private void Awake()
    {
        gameObject.tag = "Thing";
    }

    private void Start()
    {

        //Init List
        neighborList = new List<GameObject>();

        //neighbor detector
        neighborDetector = GetComponent<SphereCollider>();
        neighborDetector.isTrigger = true;
        neighborDetector.radius = neighborDetectorRadius;

        //nav mesh agent
        nmAgent = GetComponent<NavMeshAgent>();

        //Chat Ballon
        chatBalloon = gameObject.GetComponentInChildren<ChatBalloon>();

        //Instantiating Particle Object
        //TODO: add particle system prefab to each models
        explodePS = GetComponentInChildren<ParticleSystem>();

        //Sound
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.spatialBlend = 0.9f;
        audioSource.maxDistance = 35;



        InvokeRepeating("RandomSetDestination", 5f, 15f);

    }

    void RandomSetDestination()
    {
        SetTarget(RandomVec3(-40, 40));
    }

    private void Update()
    {

        //Mouse left key
        if (Input.GetMouseButtonUp(0))
        {
            SetTarget(RandomVec3(-40, 40));
            //nmAgent.SetDestination(RandomVec3(-40, 40));
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

    //TODO for yang: make this private
    public void Speak(string content, float stayLength)
    {
        Debug.Log(gameObject.name + " speaks: " + content);
        TTTEventsManager.main.SomeoneSpoke(gameObject);
        chatBalloon.SetTextAndActive(content, stayLength);
    }

    public void Speak(string content)
    {
        Speak(content, 2f);
    }

    public void Spark(Color particleColor, int numberOfParticles)
    {

        var particleMain = explodePS.main;
        particleMain.startColor = particleColor;

        var newBurst = new ParticleSystem.Burst(0f, numberOfParticles);
        explodePS.emission.SetBurst(0, newBurst);
        explodePS.Play();
        TTTEventsManager.main.SomeoneSparked(gameObject);
    }

    private void PlaySound(string soundName)
    {
        audioSource.clip = Resources.Load(soundFilePath + soundName) as AudioClip;
        audioSource.Play();
    }


    //---------------------------------------------------------------------------------
    //  EVENTS
    //---------------------------------------------------------------------------------

    private void OnMeetingSomeone(GameObject other)
    {
        //when you meet another Thing
        //do something
        Speak("I met " + other.name, 2f);
    }

    private void OnLeavingSomeone(GameObject other)
    {
        //when another Thing leaves you
        //do something
        Spark(Color.red, 15);
    }

    private void OnNeighborSpeaking()
    {
        PlaySound("glitchedtones_Robot Chatter 01");
    }

    private void OnNeigborSparkingParticles()
    {
        Speak("Hey You sparked!");
    }

    private void OnSunset()
    {
        //RotateSelf
        PlaySound("zapsplat_multimedia_game_blip_generic_tone_007_17643");
        Speak("I love sunset!", 2f);
    }

    private void OnSunrise()
    {
        PlaySound("cartoon-pinch");
    }

    //---------------------------------------------------------------------------------
    //  OTHER 
    //---------------------------------------------------------------------------------

    private void OnSomeoneSpeaking(GameObject who)
    {
        if (neighborList.Contains(who))
        {
            OnNeighborSpeaking();
        }
    }

    private void OnSomeoneSparking(GameObject who)
    {
        if (neighborList.Contains(who))
        {
            OnNeigborSparkingParticles();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Thing")
        {
            OnMeetingSomeone(other.gameObject);
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
            OnLeavingSomeone(other.gameObject);
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
        return new Vector3(Random.Range(a, b), 0f, Random.Range(a, b));
    }





}
