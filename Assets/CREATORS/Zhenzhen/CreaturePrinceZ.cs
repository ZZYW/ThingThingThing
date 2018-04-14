/*
 * 2018 APR.
 * BY ZZYW @ ASIA ART ARCHIVE
 * HI@ZZYWSTUDIO.COM
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(ThingMotor))]
public class CreaturePrinceZ : MonoBehaviour
{

    //distance from camera to object center on 3rd personn camera following mode
    [HideInInspector]
    private int cameraOffset = 15;
    ////how large is my neighbor awareness radar
    //private int neighborDetectorRadius = 10;

    //movement stuff
    private float acceleration = 6;
    private float drag = 1.8f; // the bigger, the slower
    private float mass = 0.2f; // the bigger, the heavier, the more acceleration it needs to get this moving, also can push away lighter THINGS

    //how often to get a new target to run to
    private float getNewDestinationInterval = 5; //seconds
    private int newDestinationRange = 40; // how far the new destination could be 


    public bool InWater;
    private int NeighborCount
    {
        get
        {
            return neighborList.Count;
        }
    }


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


    public int DesiredFollowDistance
    {
        get
        {
            return cameraOffset;
        }
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
			changeAlpha ();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SetRandomTarget(newDestinationRange);
        }
    }

    #region Events
    private void OnMeetingSomeone(GameObject other)
    {
        //DO STUFF
        Speak( other.name + "is nearby", 2f);
    }

    private void OnLeavingSomeone(GameObject other)
    {
        //DO STUFF
        Spark(Color.red, 15);
    }

    private void OnNeighborSpeaking()
    {
        //DO STUFF
        PlaySound("glitchedtones_Robot Chatter 03");
    }

    public void OnTouchWater()
    {
        InWater = true;
        Invoke("RescueFromWater", 60f);
        Speak("I am in water!");
    }

    public void OnLeaveWater()
    {
        InWater = false;
        Speak("I am not in water anymore!");
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
        Speak("Praise the Sun!", 2f);
    }

    private void OnSunrise()
    {
        //DO STUFF
        PlaySound("cartoon-pinch");
    }
    #endregion
    #endregion


	private void changeAlpha(){
		float tempAlpha = Mathf.Sin (Time.time)+1;
		Color tempColor = new Color(0.2F, 0.3F, 0.4F, tempAlpha);
		GameObject body = transform.Find("default").gameObject;
		if (body != null) {
			body.GetComponent<Renderer>().material.color = tempColor;
		}

	}

    //---------------------------------------------------------------------------------
    //  YOUR ZONE ENDS HERE!!!
    //---------------------------------------------------------------------------------

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
        //neighbor detector
        neighborDetector = GetComponent<SphereCollider>();
        neighborDetector.isTrigger = true;
        //neighborDetector.radius = neighborDetectorRadius;

        //motor
        motor = GetComponent<ThingMotor>();
        acceleration = Random.Range(3f, 4f);
        motor.SetAccel(acceleration);
        motor.rb.drag = drag;
        motor.rb.mass = mass;

        boxCollider = gameObject.GetComponent<BoxCollider>();

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
            ResetPosition();
        }
        Repeat();
    }





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
        if (other.gameObject.CompareTag("Thing"))
        {
            OnMeetingSomeone(other.gameObject);
            if (!neighborList.Contains(other.gameObject))
            {
                neighborList.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Thing"))
        {
            OnLeavingSomeone(other.gameObject);
            if (neighborList.Contains(other.gameObject))
            {
                neighborList.Remove(other.gameObject);
            }
        }
    }


    //---------------------------------------------------------------------------------
    //  BEHAVIOURS
    //---------------------------------------------------------------------------------

    private void SetTarget(Vector3 target)
    {
        motor.SetTarget(target);
    }

    private void SetRandomTarget(float area)
    {
        SetTarget(new Vector3(Random.Range(-area, area), 0, Random.Range(-area, area)));
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
        //Debug.Log (gameObject.name + " speaks: " + content);
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

    private void RandomSetDestination()
    {
        SetRandomTarget(newDestinationRange);
    }

    private void ResetPosition()
    {
        motor.rb.position = ThingManager.main.transform.position;
    }


    private void RescueFromWater()
    {
        if (InWater)
        {
            ResetPosition();
        }
    }


    private void UnlockSpeakCD()
    {
        speakInCD = false;
    }

}
