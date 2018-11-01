using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TOD_Data : MonoBehaviour {

    public static System.Action OnSunrise;
    public static System.Action OnSunset;

    public static TOD_Data main;

    private TOD_Sky sky;

    public bool IsDay { get; private set; }
    public bool IsNight { get; private set; }

    public float TimeNow {
        get {
            return GetComponent<TOD_Sky> ().Cycle.Hour;
        }
    }

    private void Awake () {
        if (main == null) {
            main = this;
        }
    }

    bool sunsetReported;
    bool sunriseReported;

    float reportCoolDown = 5f;

    // Use this for initialization
    void Start () {
        sky = GetComponent<TOD_Sky> ();
        IsDay = true;
        IsNight = false;
    }

    // Update is called once per frame
    void Update () {

        float hour = sky.Cycle.Hour;
        if (hour > 19f && hour < 19.2f && !sunsetReported) {
            print ("------Sunset------");
            if (OnSunset != null) OnSunset ();
            ThingConsole.LogWarning ("Sunset");
            sunsetReported = true;
            IsDay = false;
            IsNight = true;
            Invoke ("ResetSunsetReportFlag", reportCoolDown);
        }

        if (hour > 7f && hour < 7.2f && !sunriseReported) {
            print ("------Sunrise------");
            if (OnSunrise != null) OnSunrise ();
            ThingConsole.LogWarning ("Sunrise");
            IsDay = true;
            IsNight = false;
            sunriseReported = true;
            Invoke ("ResetSunriseReportFlag", reportCoolDown);
        }

    }

    void ResetSunsetReportFlag () {
        sunsetReported = false;
    }

    void ResetSunriseReportFlag () {
        sunriseReported = false;
    }
}