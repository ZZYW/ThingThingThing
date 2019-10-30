using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TOD_Data : MonoBehaviour {

    public static System.Action OnSunrise;
    public static System.Action OnSunset;

    public static TOD_Data main;

    [SerializeField] Text timeText;
    TOD_Sky sky;
    StringBuilder stringBuilder;

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
        stringBuilder = new StringBuilder ();
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

        stringBuilder.Length = 0;
        stringBuilder.AppendFormat ("\n\n{0}", sky.Cycle.DateTime.TimeOfDay.ToString ());

    }

    void ResetSunsetReportFlag () {
        sunsetReported = false;
    }

    void ResetSunriseReportFlag () {
        sunriseReported = false;
    }
}