using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent (typeof (BoxCollider))]
[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (AudioSource))]
[RequireComponent (typeof (ThingMotor))]
public class Thing : MonoBehaviour {

    public class Settings {
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

        public Settings () {
            cameraOffset = 15;
            acceleration = 4;
            drag = 1.8f;
            mass = 1f;
            chatBubbleOffsetHeight = 10;
            getNewDestinationInterval = 5;
            newDestinationRange = 40;
            alwaysFacingTarget = true;
            neighborDetectorRadius = 10;
        }
    }

    public SimpleChatBubble myChatBubble;
    public Settings settings { get; protected set; }

    [HideInInspector] public Vector3 bubbleOffsetPosition;

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

    [Header ("Your Main Material For Color Changing")]
    [SerializeField] Material mMat;

    public int DesiredFollowDistance { get { return settings.cameraOffset; } }

    private void OnEnable () {
        TTTEventsManager.OnSomeoneSpeaking += OnSomeoneSpeaking;
        TTTEventsManager.OnSomeoneSparking += OnSomeoneSparking;
        TOD_Data.OnSunset += OnSunset;
        TOD_Data.OnSunrise += OnSunrise;
    }

    private void OnDisable () {
        TTTEventsManager.OnSomeoneSpeaking -= OnSomeoneSpeaking;
        TTTEventsManager.OnSomeoneSparking -= OnSomeoneSparking;
        TOD_Data.OnSunset -= OnSunset;
        TOD_Data.OnSunrise -= OnSunrise;
        CancelInvoke ();
    }

    private void Awake () {
        speakCD = new Cooldown (Random.Range (5f, 10f));
        playSoundCD = new Cooldown (1);

        MyName = gameObject.name;
        settings = new Settings ();
        stringBuilder = new StringBuilder ();
        gameObject.tag = thingTag;
        gameObject.layer = 14;
        neighborList = new List<GameObject> ();
        TTTAwake ();
    }

    private void Start () {
        //neighbor detector
        // neighborDetector = GetComponent<SphereCollider> ();
        // neighborDetector.isTrigger = true;
        //no longer use sphere collider as neighbor detector
        if (GetComponent<SphereCollider> ()) {
            Destroy (GetComponent<SphereCollider> ());
        }

        //motor
        motor = GetComponent<ThingMotor> ();
        motor.SetAccel (settings.acceleration * 4);
        motor.rb.drag = settings.drag;
        motor.rb.mass = settings.mass;
        motor.FacingTarget (settings.alwaysFacingTarget);

        //Instantiating Particle Object
        explodePS = GetComponentInChildren<ParticleSystem> ();
        if (explodePS == null) {
            ThingConsole.LogWarning (FormatString ("{0} doesn't have a particle system?!", MyName));
        }

        //Sound
        audioSource = gameObject.GetComponent<AudioSource> ();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.bypassListenerEffects = false;
        audioSource.spatialBlend = 1f;
        audioSource.maxDistance = 35;
        audioSource.dopplerLevel = 5;

        TTTStart ();

        speakCD.GoCooldown ();

    }
    private void Update () {
        //check cooldown
        speakCD.Check ();
        playSoundCD.Check ();

        //check position
        if (Vector3.Distance (transform.position, transform.parent.position) > 200) {
            ResetPosition ();
        }

        //check neighbors
        foreach (GameObject t in ThingManager.main.AllThings) {
            float dist = Vector3.Distance (transform.position, t.transform.position);
            if (dist < settings.neighborDetectorRadius) {
                if (!neighborList.Contains (t)) {
                    neighborList.Add (t);
                    OnMeetingSomeone (t);
                }
            } else {
                if (neighborList.Contains (t)) {
                    neighborList.Remove (t);
                    OnLeavingSomeone (t);
                }
            }
        }

        TTTUpdate ();
    }

    private void OnSomeoneSpeaking (GameObject who) {
        if (neighborList.Contains (who)) {
            OnNeighborSpeaking ();
        }
    }

    private void OnSomeoneSparking (GameObject who) {
        if (neighborList.Contains (who)) {
            OnNeigborSparkingParticles ();
        }
    }

    //use string builder to concat string to avoid memory leak
    private string FormatString (string format, params object[] args) {
        stringBuilder.Length = 0;
        stringBuilder.AppendFormat (format, args);
        return stringBuilder.ToString ();
    }

    private void RescueFromWater () {
        if (InWater) {
            ResetPosition ();
            ThingConsole.LogWarning (FormatString ("{0} is rescued from water", MyName));
        }
    }

    public void OnWaterEnter () {
        InWater = true;
        Invoke ("RescueFromWater", 60f);
        OnTouchWater ();
    }

    public void OnWaterExit () {
        InWater = false;
        CancelInvoke ("RescueFromWater");
        OnLeaveWater ();
        ThingConsole.Log (FormatString ("<color=orange>{0}</color> left <color=blue>water</color>.", MyName));
    }

    protected void SetTarget (Vector3 target) {
        if (!stopWalkingAround) {
            motor.SetTarget (target);
            ThingConsole.Log (FormatString ("<color=orange>{0}</color> gained a <color=red>new</color> target.", MyName));
        }
    }

    protected void StopMoving () {
        stopWalkingAround = true;
        motor.Stop ();
        ThingConsole.Log (FormatString ("{0} stopped moving", MyName));
    }

    protected void StopMoving (float seconds) {
        StopMoving ();
        Invoke ("RestartWalking", seconds);
    }

    protected void Mute () {
        stopTalking = true;
        ThingConsole.LogWarning (FormatString ("{0} is being muted.", MyName));
    }

    protected void DeMute () {
        stopTalking = false;
        ThingConsole.Log (FormatString ("{0} can speak again", MyName));
    }

    protected void RestartWalking () {
        stopWalkingAround = false;
    }

    protected void SetRandomTarget (float area) {
        SetTarget (new Vector3 (Random.Range (-area, area), 0, Random.Range (-area, area)));
    }

    protected void AddForce (Vector3 f) {
        motor.rb.AddForce (f);
    }

    protected void SetScale (Vector3 newScale) {
        transform.localScale = newScale;
    }

    protected void Speak (string content) {
        if (myChatBubble == null) return;
        if (stopTalking) return;
        if (speakCD.inCD) return;

        TTTEventsManager.main.SomeoneSpoke (gameObject);
        myChatBubble.Speak (content);
        ThingConsole.Log(FormatString("<color=orange>{0}</color> is speaking <i>{1}</i>", MyName, content));

        speakCD.GoCooldown ();
    }

    protected void Spark (Color particleColor, int numberOfParticles) {

        if (explodePS == null) {
            explodePS = GetComponentInChildren<ParticleSystem> ();
            if (explodePS == null) return;
        }

        ParticleSystem.MainModule particleMain = explodePS.main;
        particleMain.startColor = particleColor;
        var newBurst = new ParticleSystem.Burst (0f, numberOfParticles);
        explodePS.emission.SetBurst (0, newBurst);
        explodePS.Play ();
        TTTEventsManager.main.SomeoneSparked (gameObject);

        ThingConsole.Log (FormatString ("<color=orange>{0}</color> sparked", MyName));
    }

    protected void CreateCube () {
        GameObject acube = GameObject.CreatePrimitive (PrimitiveType.Cube);
        acube.layer = 12;

        acube.transform.localScale = Vector3.one / 4;
        acube.transform.position = transform.position;

        if (generatedCubeContainer == null) {
            generatedCubeContainer = new GameObject ();
            generatedCubeContainer.name = "Generated Cube Container";
            generatedCubeContainer.AddComponent<ChildrenCounter> ();
        }

        acube.transform.parent = generatedCubeContainer.transform;
        generatedCubeContainer.GetComponent<ChildrenCounter> ().list.Add (acube);

        acube.AddComponent<Rigidbody> ();
        acube.AddComponent<ProducedCube> ().Init (settings.myCubeColor);

    }

    protected void ResetColor () {
        if (mMat == null) return;
        mMat.color = originalColor;
        ThingConsole.Log (FormatString ("{0} reset its own color", MyName));
    }

    protected void ChangeColor (Color c) {
        if (mMat == null) return;
        mMat.SetColor (matColor, c);
    }

    protected void PlaySound (int soundFileID) {
        if (soundFileID < 1 || soundFileID > 102) Debug.LogWarning ("sound file id exceed the range, range is 1->102, you are calling " + soundFileID);
        PlaySound (soundFileID.ToString ());
    }

    protected void PlaySound (string soundFileName) {
        if (playSoundCD.inCD) return;
        playSoundCD.GoCooldown ();
        audioSource.pitch = Random.Range (1.0f, 2.2f);
        if (audioSource.isPlaying) return;
        string soundUrl = soundFilePath + soundFileName;
        audioSource.clip = Resources.Load (soundUrl) as AudioClip;
        if (audioSource.clip != null) {
            audioSource.Play ();
            Spark (Color.white, 5);
        }
    }

    protected ThingMotor GetMotor () {
        return motor;
    }

    protected void RandomSetDestination () {
        SetRandomTarget (settings.newDestinationRange);
    }

    protected void ResetPosition () {
        motor.rb.position = ThingManager.main.GetSpawnPosition ();
        motor.rb.velocity = Vector3.zero;
    }

    //VIRTUAL
    protected virtual void TTTAwake () { }
    protected virtual void TTTStart () { }
    protected virtual void TTTUpdate () { }
    protected virtual void OnMeetingSomeone (GameObject other) { }
    protected virtual void OnLeavingSomeone (GameObject other) { }
    protected virtual void OnNeighborSpeaking () { }
    protected virtual void OnNeigborSparkingParticles () { }
    protected virtual void OnTouchWater () { }
    protected virtual void OnLeaveWater () { }
    protected virtual void OnSunset () { }
    protected virtual void OnSunrise () { }

}