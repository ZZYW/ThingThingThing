using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(ThingMotor))]
public class Creature : MonoBehaviour
{

    // YOU CAN TWEAK THOSE TO CUSTOMIZE YOUR OWN AVATAR
    //distance from camera to object center on 3rd personn camera following mode
    [HideInInspector]
    private int desiredFollowDistance = 3;
    //how large is my neighbor awareness radar
    private int neighborDetectorRadius = 10;
    //nav mesh agent
    private float moveSpeed = 4;
    private float getNewDestinationInterval = 15; //seconds


    //flags
    private float speakCDLength;
    private bool speakInCD;


    private NavMeshAgent nmAgent;
    private Rigidbody rb;
    private ThingMotor motor;
    private SphereCollider neighborDetector;
    private ChatBalloon chatBalloon;
    private BoxCollider boxCollider;
    private ParticleSystem explodePS;
    private AudioSource audioSource;
    private List<GameObject> neighborList;
    private string soundFilePath = "Sounds/";
    private int NeighborCount
    {
        get
        {
            return neighborList.Count;
        }
    }
    public int DesiredFollowDistance
    {
        get
        {
            return desiredFollowDistance;
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
        rb = GetComponent<Rigidbody>();
        rb.mass = 0.1f;

        //neighbor detector
        neighborDetector = GetComponent<SphereCollider>();
        neighborDetector.isTrigger = true;
        neighborDetector.radius = neighborDetectorRadius;

        //nav mesh agent
        nmAgent = GetComponent<NavMeshAgent>();
        motor.MaxSpeed = moveSpeed;

        nmAgent.enabled = false;

        //motor
        motor = GetComponent<ThingMotor>();

        boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.enabled = true;

        //Chat Ballon
        chatBalloon = gameObject.GetComponentInChildren<ChatBalloon>();
        speakCDLength = Random.Range(8f, 13f);

        //Instantiating Particle Object
        //TODO: add particle system prefab to each models
        explodePS = GetComponentInChildren<ParticleSystem>();

        //Sound
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.spatialBlend = 0.9f;
        audioSource.maxDistance = 35;


        Init();
    }
    private void Update()
    {
        //avoid dropping
        if (transform.position.y < -9)
        {
            rb.position = ThingManager.main.transform.position;
        }

        Repeat();
    }

    void RandomSetDestination()
    {
        SetTarget(RandomVec3(-40, 40));
    }



    #region THING CUSTOMIZATION AREA
    //---------------------------------------------------------------------------------
    //  YOUR ZONE! DO MOST OF THE STUFF HERE
    //---------------------------------------------------------------------------------

    //Only execute once at the start of the story
    void Init()
    {
        Speak("hello");
        InvokeRepeating("RandomSetDestination", 1, getNewDestinationInterval);
    }


    void Repeat()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Jump(3, 1f, 500);
        }

        if (TOD_Data.main.IsNight)
        {

        }

        if (TOD_Data.main.IsDay)
        {

        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RandomSetDestination();
        }
    }

    #region Events
    private void OnMeetingSomeone(GameObject other)
    {
        //DO STUFF
        Speak("I met " + other.name, 2f);
    }

    private void OnLeavingSomeone(GameObject other)
    {
        //DO STUFF
        Spark(Color.red, 15);
    }

    private void OnNeighborSpeaking()
    {
        //DO STUFF
        PlaySound("glitchedtones_Robot Chatter 01");
    }

    private void OnNeigborSparkingParticles()
    {
        //DO STUFF
        Speak("Hey You sparked!");
    }

    private void OnSunset()
    {
        //DO STUFF
        PlaySound("zapsplat_multimedia_game_blip_generic_tone_007_17643");
        Speak("I love sunset!", 2f);
    }

    private void OnSunrise()
    {
        //DO STUFF
        PlaySound("cartoon-pinch");
    }
    #endregion
    #endregion

    //---------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------
    // ALERT:  DEVELOPER STUFF 
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


    //---------------------------------------------------------------------------------
    //  BEHAVIOURS
    //---------------------------------------------------------------------------------
    #region Behaviour Implementations
    private void SetTarget(Vector3 target)
    {
        //nmAgent.SetDestination(target);

        motor.Target = target;

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
        if (speakInCD) return;
        Debug.Log(gameObject.name + " speaks: " + content);
        TTTEventsManager.main.SomeoneSpoke(gameObject);
        chatBalloon.SetTextAndActive(content, stayLength);
        speakInCD = true;
        Invoke("UnlockSpeakCD", speakCDLength);
    }

    private void Speak(string content)
    {
        Speak(content, 2f);
    }

    private void Spark(Color particleColor, int numberOfParticles)
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

    private void Jump(int times, float interval, float upForce)
    {
        if (times < 1 || interval < 0.1f || upForce < 1f) return;
        //Debug.Log("Jump!");
        InitJump();
        StartCoroutine(CJump(times, interval, upForce));
    }

    private IEnumerator CJump(int times, float interval, float upForce)
    {
        for (int i = 0; i < times; i++)
        {
            Debug.Log(i);
            rb.AddForce(Vector3.up * upForce);
            yield return new WaitForSeconds(interval);
        }
        Invoke("AfterJump", 1f);
    }
    #endregion
    //TODO: try add mesh coolider duringjump
    void InitJump()
    {
        boxCollider.enabled = true;
        nmAgent.enabled = false;
        rb.isKinematic = false;
    }

    void AfterJump()
    {
        nmAgent.enabled = true;
        boxCollider.enabled = false;
    }

    void UnlockSpeakCD()
    {
        speakInCD = false;
    }


    public static Vector3 RandomVec3(float a, float b)
    {
        return new Vector3(Random.Range(a, b), 0f, Random.Range(a, b));
    }







}
