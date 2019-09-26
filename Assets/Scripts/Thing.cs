using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Thing : MonoBehaviour
{

    public class Settings
    {
        public int cameraOffset;
        public float acceleration;
        public float drag;
        public float mass;
        public float chatBubbleOffsetHeight;
        public float getNewDestinationInterval;
        public int newDestinationRange;
        public bool alwaysFacingTarget;
        public Color myCubeColor;
        public int neighborDetectorRadius;

        public Settings()
        {
            cameraOffset = 15;
            acceleration = 4;
            drag = 1.8f;
            mass = 20f;
            chatBubbleOffsetHeight = 10;
            getNewDestinationInterval = 5;
            newDestinationRange = 40;
            alwaysFacingTarget = true;
            neighborDetectorRadius = 10;
        }
    }

    public SimpleChatBubble myChatBubble;
    public Settings settings { get; protected set; }



    protected bool InWater { get; private set; }
    protected int NeighborCount { get { return neighborList.Count; } }
    protected string MyName { get; private set; }

    //cool down stuff to avoid crash
    Cooldown speakCD;
    Cooldown playSoundCD;
    // float speakCooldown;
    // bool speakInCD;
    // float spokeTimeStamp;

    float detectEnterExitCooldown;
    bool detectingEnter;
    bool detectingExit;

    bool stopWalkingAround;
    bool stopTalking;
    ThingMotor motor;
    SphereCollider neighborDetector;

    ParticleSystem explodePS;
    AudioSource audioSource;
    List<GameObject> neighborList;

    static string soundFilePath = "Sounds/";
    static string matColor = "_Color";
    static GameObject generatedCubeContainer;
    static string thingTag = "Thing";

    Color originalColor;
    StringBuilder stringBuilder;

    Material mMat;

    public int DesiredFollowDistance { get { return settings.cameraOffset; } }

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
        CancelInvoke();
    }

    private void Awake()
    {
        speakCD = new Cooldown(Random.Range(5f, 10f));
        playSoundCD = new Cooldown(1);

  
        Instantiate(ResourceManager.main.sparkPSPrefab, transform, false);


        MyName = gameObject.name;
        settings = new Settings();
        stringBuilder = new StringBuilder();
        gameObject.tag = thingTag;
        gameObject.layer = 14;
        neighborList = new List<GameObject>();
        ThingAwake();
    }

    private void Start()
    {
        //add essential part
        gameObject.AddComponent<MeshRenderer>();
        gameObject.AddComponent<MeshFilter>().mesh = ResourceManager.main.cubeMesh.mesh;
        gameObject.AddComponent<AudioSource>();
        gameObject.AddComponent<BoxCollider>();
        var rb = gameObject.AddComponent<Rigidbody>();


        //get the material
        mMat = GetComponent<Renderer>().material;


        //motor
        motor = gameObject.AddComponent<ThingMotor>();
        motor.SetAccel(settings.acceleration * 4);
        motor.rb.drag = settings.drag;
        motor.rb.mass = settings.mass;
        motor.FacingTarget(settings.alwaysFacingTarget);

        //Instantiating Particle Object
        explodePS = GetComponentInChildren<ParticleSystem>();

        //Sound
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.bypassListenerEffects = false;
        audioSource.spatialBlend = 1f;
        audioSource.maxDistance = 35;
        audioSource.dopplerLevel = 5;

        ThingStart();

        speakCD.GoCooldown();

    }
    private void Update()
    {
        //check cooldown
        speakCD.Check();
        playSoundCD.Check();


        //check neighbors
        foreach (GameObject t in ThingManager.main.AllThings)
        {
            float dist = Vector3.Distance(transform.position, t.transform.position);
            if (dist < settings.neighborDetectorRadius)
            {
                if (!neighborList.Contains(t))
                {
                    neighborList.Add(t);
                    OnMeetingSomeone(t);
                }
            }
            else
            {
                if (neighborList.Contains(t))
                {
                    neighborList.Remove(t);
                    OnLeavingSomeone(t);
                }
            }
        }

        ThingUpdate();
    }

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

    //use string builder to concat string to avoid memory leak
    private string FormatString(string format, params object[] args)
    {
        stringBuilder.Length = 0;
        stringBuilder.AppendFormat(format, args);
        return stringBuilder.ToString();
    }


    public void OnWaterEnter()
    {
        InWater = true;
        Invoke("RescueFromWater", 60f);
        OnTouchWater();
    }

    public void OnWaterExit()
    {
        InWater = false;
        CancelInvoke("RescueFromWater");
        OnLeaveWater();
    }

    protected void SetTarget(Vector3 target)
    {
        if (!stopWalkingAround)
        {
            motor.SetTarget(target);
        }
    }

    protected void StopMoving()
    {
        stopWalkingAround = true;
        motor.Stop();
    }

    protected void StopMoving(float seconds)
    {
        StopMoving();
        Invoke("RestartWalking", seconds);
    }

    protected void Mute()
    {
        stopTalking = true;
    }

    protected void DeMute()
    {
        stopTalking = false;
    }

    protected void RestartWalking()
    {
        stopWalkingAround = false;
    }

    protected void SetRandomTarget(float area)
    {
        SetTarget(new Vector3(Random.Range(-area, area), 0, Random.Range(-area, area)));
    }

    protected void AddForce(Vector3 f)
    {
        motor.rb.AddForce(f);
    }

    protected void SetScale(Vector3 newScale)
    {
        transform.localScale = newScale;
    }

    protected void Speak(string content)
    {
        if (myChatBubble == null) return;
        if (stopTalking) return;
        if (speakCD.inCD) return;

        TTTEventsManager.main.SomeoneSpoke(gameObject);
        myChatBubble.Speak(content);
        speakCD.GoCooldown();
    }

    protected void Spark(Color particleColor, int numberOfParticles)
    {

        if (explodePS == null)
        {
            explodePS = GetComponentInChildren<ParticleSystem>();
            if (explodePS == null) return;
        }

        ParticleSystem.MainModule particleMain = explodePS.main;
        particleMain.startColor = particleColor;
        var newBurst = new ParticleSystem.Burst(0f, numberOfParticles);
        explodePS.emission.SetBurst(0, newBurst);
        explodePS.Play();
        TTTEventsManager.main.SomeoneSparked(gameObject);

    }

    protected void CreateChildren()
    {
        GameObject acube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        acube.layer = 12;

        acube.transform.localScale = Vector3.one / 4;
        acube.transform.position = transform.position;

        if (generatedCubeContainer == null)
        {
            generatedCubeContainer = new GameObject();
            generatedCubeContainer.name = MyName + "'s child";
            generatedCubeContainer.AddComponent<ChildrenCounter>();
        }

        acube.transform.parent = generatedCubeContainer.transform;
        generatedCubeContainer.GetComponent<ChildrenCounter>().list.Add(acube);

        acube.AddComponent<Rigidbody>();
        acube.AddComponent<ProducedCube>().Init(settings.myCubeColor);

    }

    protected void ResetColor()
    {
        if (mMat == null) return;
        mMat.color = originalColor;
    }

    protected void ChangeColor(Color c)
    {
        if (mMat == null) return;
        mMat.SetColor(matColor, c);
    }

    protected void PlaySound()
    {
        if (playSoundCD.inCD) return;
        playSoundCD.GoCooldown();
        if (audioSource.isPlaying) return;
        if (audioSource.clip != null)
        {
            audioSource.Play();
        }
    }

    protected ThingMotor GetMotor()
    {
        return motor;
    }

    protected void RandomSetDestination()
    {
        SetRandomTarget(settings.newDestinationRange);
    }




    private void AddCubeMesh()
    {
        Vector3[] vertices = {
            new Vector3 (0, 0, 0),
            new Vector3 (1, 0, 0),
            new Vector3 (1, 1, 0),
            new Vector3 (0, 1, 0),
            new Vector3 (0, 1, 1),
            new Vector3 (1, 1, 1),
            new Vector3 (1, 0, 1),
            new Vector3 (0, 0, 1),
        };

        int[] triangles = new int[]
        {
            3, 1, 0,        3, 2, 1,        // Bottom	
	        7, 5, 4,        7, 6, 5,        // Left
	        11, 9, 8,       11, 10, 9,      // Front
	        15, 13, 12,     15, 14, 13,     // Back
	        19, 17, 16,     19, 18, 17,	    // Right
	        23, 21, 20,     23, 22, 21,	    // Top
        };

        Mesh mesh = gameObject.AddComponent<MeshFilter>().mesh;



        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        //  mesh.Optimize();
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.RecalculateTangents();
    }

    //VIRTUAL
    protected virtual void ThingAwake() { }
    protected virtual void ThingStart() { }
    protected virtual void ThingUpdate() { }
    protected virtual void OnMeetingSomeone(GameObject other) { }
    protected virtual void OnLeavingSomeone(GameObject other) { }
    protected virtual void OnNeighborSpeaking() { }
    protected virtual void OnNeigborSparkingParticles() { }
    protected virtual void OnTouchWater() { }
    protected virtual void OnLeaveWater() { }
    protected virtual void OnSunset() { }
    protected virtual void OnSunrise() { }

}