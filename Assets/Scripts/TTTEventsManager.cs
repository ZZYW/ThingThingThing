using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the center of all broadcasting messages
/// </summary>
public class TTTEventsManager : MonoBehaviour
{


    public delegate void CreatureAction(GameObject who);
    public static event CreatureAction OnSomeoneSpeaking;
    public static event CreatureAction OnSomeoneSparking;

    public static TTTEventsManager main;

    //public delegate void SpeakListener(GameObject which);


    private void Awake()
    {
        if (main == null)
        {
            main = this;
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SomeoneSparked(GameObject who)
    {
        if (OnSomeoneSparking != null) OnSomeoneSparking(who);
    }

    public void SomeoneSpoke(GameObject who)
    {
        if (OnSomeoneSpeaking != null) OnSomeoneSpeaking(who);
    }
}
