/*
 * 2018 APR.
 * BY ZZYW @ ASIA ART ARCHIVE
 * ZHENZHEN / YANG WANG
 * HI@ZZYWSTUDIO.COM
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

/*
 * 
 _________  ___   ___    ________  ___   __   _______      _________  ___   ___    ________  ___   __   _______      _________  ___   ___    ________  ___   __   _______     
/________/\/__/\ /__/\  /_______/\/__/\ /__/\/______/\    /________/\/__/\ /__/\  /_______/\/__/\ /__/\/______/\    /________/\/__/\ /__/\  /_______/\/__/\ /__/\/______/\    
\__.::.__\/\::\ \\  \ \ \__.::._\/\::\_\\  \ \::::__\/__  \__.::.__\/\::\ \\  \ \ \__.::._\/\::\_\\  \ \::::__\/__  \__.::.__\/\::\ \\  \ \ \__.::._\/\::\_\\  \ \::::__\/__  
   \::\ \   \::\/_\ .\ \   \::\ \  \:. `-\  \ \:\ /____/\    \::\ \   \::\/_\ .\ \   \::\ \  \:. `-\  \ \:\ /____/\    \::\ \   \::\/_\ .\ \   \::\ \  \:. `-\  \ \:\ /____/\ 
    \::\ \   \:: ___::\ \  _\::\ \__\:. _    \ \:\\_  _\/     \::\ \   \:: ___::\ \  _\::\ \__\:. _    \ \:\\_  _\/     \::\ \   \:: ___::\ \  _\::\ \__\:. _    \ \:\\_  _\/ 
     \::\ \   \: \ \\::\ \/__\::\__/\\. \`-\  \ \:\_\ \ \      \::\ \   \: \ \\::\ \/__\::\__/\\. \`-\  \ \:\_\ \ \      \::\ \   \: \ \\::\ \/__\::\__/\\. \`-\  \ \:\_\ \ \ 
      \__\/    \__\/ \::\/\________\/ \__\/ \__\/\_____\/       \__\/    \__\/ \::\/\________\/ \__\/ \__\/\_____\/       \__\/    \__\/ \::\/\________\/ \__\/ \__\/\_____\/ 
                                                                                                                                                                              
 */

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(ThingMotor))]
public class Creature : MonoBehaviour
{
    
    //distance from camera to object center on 3rd personn camera following mode
    [HideInInspector]
    private int followCameraObserverDistance = 15;
    //how large is my neighbor awareness radar
    private int neighborDetectorRadius = 10;

    //movement stuff
    private float acceleration = 5;
    private float drag = 1.8f; // the bigger, the slower
    private float mass = 0.2f; // the bigger, the heavier, the more acceleration it needs to get this moving, also can push away lighter THINGS
    //how often to get a new target to run to
    private float getNewDestinationInterval = 3; //seconds


    //flags
    private float speakCDLength;
    private bool speakInCD;
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
            return followCameraObserverDistance;
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
        neighborList = new List<GameObject>();
    }

    private void Start()
    {
        //Init List


        //neighbor detector
        neighborDetector = GetComponent<SphereCollider>();
        neighborDetector.isTrigger = true;
        neighborDetector.radius = neighborDetectorRadius;

        //motor
        motor = GetComponent<ThingMotor>();
        acceleration = Random.Range(3f, 4f);
        motor.SetAccel(acceleration);
        motor.rb.drag = drag;
        motor.rb.mass = mass;

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
            motor.rb.position = ThingManager.main.transform.position;
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
        InvokeRepeating("RandomSetDestination", 0, getNewDestinationInterval);
    }


    void Repeat()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {

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
        motor.SetTarget(target);
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


    #endregion


    void UnlockSpeakCD()
    {
        speakInCD = false;
    }


    public static Vector3 RandomVec3(float a, float b)
    {
        return new Vector3(Random.Range(a, b), 0f, Random.Range(a, b));
    }







}
