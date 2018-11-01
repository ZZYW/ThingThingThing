using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent (typeof (SphereCollider))]
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

        public Settings () {
            cameraOffset = 15;
            acceleration = 4;
            drag = 1.8f;
            mass = 0.2f;
            chatBubbleOffsetHeight = 3;
            getNewDestinationInterval = 5;
            newDestinationRange = 40;
            alwaysFacingTarget = true;
        }
    }

    public Settings settings { get; protected set; }
    protected bool InWater { get; private set; }
    protected int NeighborCount { get { return neighborList.Count; } }
    protected string MyName { get; private set; }

    private static float speakCDLength = 5; //seconds
    [SerializeField] private bool speakInCD;
    private float spokeTimeStamp;
    private bool stopWalkingAround;
    private bool stopTalking;
    private ThingMotor motor;
    private SphereCollider neighborDetector;
    public SimpleChatBubble chatBubble;
    private ParticleSystem explodePS;
    private AudioSource audioSource;
    private List<GameObject> neighborList;
    private static string soundFilePath = "Sounds/";
    private static string matColor = "_Color";
    private Color originalColor;
    private StringBuilder stringBuilder;

    [Header ("Your Main Material For Color Changing")]
    [SerializeField] private Material mMat;
    private static GameObject generatedCubeContainer;
    private static string thingTag = "Thing";
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
        MyName = gameObject.name;
        settings = new Settings ();
        stringBuilder = new StringBuilder ();
        gameObject.tag = thingTag;
        neighborList = new List<GameObject> ();
        TTTAwake ();
    }

    private void Start () {
        //neighbor detector
        neighborDetector = GetComponent<SphereCollider> ();
        neighborDetector.isTrigger = true;

        //motor
        motor = GetComponent<ThingMotor> ();
        motor.SetAccel (settings.acceleration);
        motor.rb.drag = settings.drag;
        motor.rb.mass = settings.mass;
        motor.FacingTarget (settings.alwaysFacingTarget);

        //Instantiating Particle Object
        explodePS = GetComponentInChildren<ParticleSystem> ();
        if (explodePS == null) {
            stringBuilder.Length = 0;
            stringBuilder.AppendFormat ("{0} doesn't have a particle system?!", MyName);
            ThingConsole.LogWarning (stringBuilder.ToString ());
        }

        //Sound
        audioSource = gameObject.GetComponent<AudioSource> ();
        audioSource.rolloffMode = AudioRolloffMode.Linear;
        audioSource.spatialBlend = 1f;
        audioSource.maxDistance = 50;

        TTTStart ();
    }
    private void Update () {

        if (speakInCD && Time.time - spokeTimeStamp > speakCDLength) {
            speakInCD = false;
        }

        if (transform.position.y < -9 || transform.position.y > 157) {
            ResetPosition ();
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

    private void OnTriggerEnter (Collider other) {
        if (other.gameObject.CompareTag (thingTag)) {
            OnMeetingSomeone (other.gameObject);
            //  ThingConsole.Log (gameObject.name + " is meeting " + other.name);
            if (!neighborList.Contains (other.gameObject)) {
                neighborList.Add (other.gameObject);
            }
        }
    }

    private void OnTriggerExit (Collider other) {
        if (other.gameObject.CompareTag (thingTag)) {
            OnLeavingSomeone (other.gameObject);
            if (neighborList.Contains (other.gameObject)) {
                neighborList.Remove (other.gameObject);
            }
        }
    }

    private void RescueFromWater () {
        if (InWater) {
            ResetPosition ();
            stringBuilder.Length = 0;
            stringBuilder.AppendFormat ("{0} is rescued from water", MyName);
            ThingConsole.LogWarning (stringBuilder.ToString ());
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

        stringBuilder.Length = 0;
        stringBuilder.AppendFormat ("{0} left water.", MyName);
        ThingConsole.Log (stringBuilder.ToString ());

    }

    protected void SetTarget (Vector3 target) {
        if (!stopWalkingAround) {
            motor.SetTarget (target);
        }
    }

    protected void StopMoving () {
        stopWalkingAround = true;
        motor.Stop ();
        stringBuilder.Length = 0;
        stringBuilder.AppendFormat ("{0} stopped moving", MyName);
        ThingConsole.Log (stringBuilder.ToString ());
    }

    protected void StopMoving (float seconds) {
        StopMoving ();
        Invoke ("RestartWalking", seconds);
    }

    protected void Mute () {
        stopTalking = true;
        stringBuilder.Length = 0;
        stringBuilder.AppendFormat ("{0} is being muted.", MyName);
        ThingConsole.LogWarning (stringBuilder.ToString ());
    }

    protected void DeMute () {
        stopTalking = false;
        stringBuilder.Length = 0;
        stringBuilder.AppendFormat ("{0} can speak again", MyName);
        ThingConsole.Log (stringBuilder.ToString ());
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
        if (stopTalking) return;
        if (speakInCD) return;

        TTTEventsManager.main.SomeoneSpoke (gameObject);

        chatBubble.Speak (content);

        speakInCD = true;
        spokeTimeStamp = Time.time;

        stringBuilder.Length = 0;
        stringBuilder.AppendFormat ("<color=orange>{0}</color> is speaking {1}", MyName, content);
        ThingConsole.Log (stringBuilder.ToString ());

    }

    protected void Spark (Color particleColor, int numberOfParticles) {
        if (explodePS == null) {
            explodePS = GetComponentInChildren<ParticleSystem> ();
        }

        ParticleSystem.MainModule particleMain = explodePS.main;
        particleMain.startColor = particleColor;
        var newBurst = new ParticleSystem.Burst (0f, numberOfParticles);
        explodePS.emission.SetBurst (0, newBurst);
        explodePS.Play ();
        TTTEventsManager.main.SomeoneSparked (gameObject);
    }

    protected void CreateCube () {
        GameObject acube = GameObject.CreatePrimitive (PrimitiveType.Cube);

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
        stringBuilder.Length = 0;
        stringBuilder.AppendFormat ("{0} reset its own color", MyName);
        ThingConsole.Log (stringBuilder.ToString ());
    }

    protected void ChangeColor (Color c) {
        if (mMat == null) return;
        mMat.SetColor (matColor, c);
    }

    protected int PlaySound (int soundFileID) {
        if (soundFileID < 1 || soundFileID > 102) Debug.LogWarning ("sound file id exceed the range, range is 1->102, you are calling " + soundFileID);
        return PlaySound (soundFileID.ToString ());
    }

    protected int PlaySound (string soundFileName) {
        if (audioSource.isPlaying) return 2;
        string soundUrl = soundFilePath + soundFileName;
        audioSource.clip = Resources.Load (soundUrl) as AudioClip;
        if (audioSource.clip != null) {
            audioSource.Play ();
            return 0;
        } else {
            return 1;
        }
    }

    protected ThingMotor GetMotor () {
        return motor;
    }

    protected void RandomSetDestination () {
        SetRandomTarget (settings.newDestinationRange);
    }

    protected void ResetPosition () {
        motor.rb.position = ThingManager.main.transform.position;
        // stringBuilder.Length = 0;
        // stringBuilder.AppendFormat ("{0} position was reset", MyName);
        // ThingConsole.Log (stringBuilder.ToString ());
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