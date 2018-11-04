using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

// [RequireComponent (typeof (SphereCollider))]
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
            mass = 0.2f;
            chatBubbleOffsetHeight = 3;
            getNewDestinationInterval = 5;
            newDestinationRange = 40;
            alwaysFacingTarget = true;
            neighborDetectorRadius = 5;
        }
    }

    public SimpleChatBubble myChatBubble;
    public Settings settings { get; protected set; }

    [HideInInspector] public Vector3 bubbleOffsetPosition;

    protected bool InWater { get; private set; }
    protected int NeighborCount { get { return neighborList.Count; } }
    protected string MyName { get; private set; }

    //cool down stuff to avoid crash
    private float speakCooldown;
    private bool speakInCD;
    private float spokeTimeStamp;

    private float detectEnterExitCooldown;
    private bool detectingEnter;
    private bool detectingExit;

    private bool stopWalkingAround;
    private bool stopTalking;
    private ThingMotor motor;
    private SphereCollider neighborDetector;

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
        speakCooldown = Random.Range (5f, 20f);
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
        motor.SetAccel (settings.acceleration);
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
        audioSource.rolloffMode = AudioRolloffMode.Linear;
        audioSource.spatialBlend = 1f;
        audioSource.maxDistance = 50;

        TTTStart ();
    }
    private void Update () {

        if (speakInCD && Time.time - spokeTimeStamp > speakCooldown) {
            speakInCD = false;
        }

        if (transform.position.y < -9 || transform.position.y > 157) {
            ResetPosition ();
        }

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

    // private void OnTriggerEnter (Collider other) {
    //     if (other.gameObject.CompareTag (thingTag)) {
    //         OnMeetingSomeone (other.gameObject);
    //         //  ThingConsole.Log (gameObject.name + " is meeting " + other.name);
    //         if (!neighborList.Contains (other.gameObject)) {
    //             neighborList.Add (other.gameObject);
    //         }
    //     }
    // }

    // private void OnTriggerExit (Collider other) {
    //     if (other.gameObject.CompareTag (thingTag)) {
    //         OnLeavingSomeone (other.gameObject);
    //         if (neighborList.Contains (other.gameObject)) {
    //             neighborList.Remove (other.gameObject);
    //         }
    //     }
    // }

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
        if (speakInCD) return;

        TTTEventsManager.main.SomeoneSpoke (gameObject);

        myChatBubble.Speak (content);

        speakInCD = true;
        spokeTimeStamp = Time.time;

        ThingConsole.Log (FormatString ("<color=orange>{0}</color> is speaking <i>{1}</i>", MyName, content));

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