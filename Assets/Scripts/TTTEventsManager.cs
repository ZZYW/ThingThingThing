using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the center of all broadcasting messages
/// </summary>
public class TTTEventsManager : MonoBehaviour {

    public delegate void CreatureAction (GameObject who);
    public static event CreatureAction OnSomeoneSpeaking;
    public static event CreatureAction OnSomeoneSparking;

    public static TTTEventsManager main;

    public class CDer {

        public bool inCD;
        public float timeStamp;
        public readonly float cdTime = 0.01f;

        public void Check () {
            if (Time.time - timeStamp > cdTime && inCD) {
                inCD = false;
            }
        }
        public void Cooldown () {
            inCD = true;
            timeStamp = Time.time;
        }
    }

    CDer sparkCDer;
    CDer talkCDer;

    //public delegate void SpeakListener(GameObject which);

    private void Awake () {
        if (main == null) {
            main = this;
        }
        talkCDer = new CDer ();
        sparkCDer = new CDer ();
    }

    void Update () {
        talkCDer.Check ();
        sparkCDer.Check ();
    }

    public void SomeoneSparked (GameObject who) {
        if (!sparkCDer.inCD) { sparkCDer.Cooldown (); } else { return; }
        if (OnSomeoneSparking != null) OnSomeoneSparking (who);
    }

    public void SomeoneSpoke (GameObject who) {
        if (!talkCDer.inCD) { talkCDer.Cooldown (); } else { return; }
        if (OnSomeoneSpeaking != null) OnSomeoneSpeaking (who);
    }
}