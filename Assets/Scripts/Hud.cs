using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Hud : MonoBehaviour
{



    public static Hud main;

     int cubeNumber;
     float time;



    public Text myText;


    string content;

    private void Awake()
    {
        main = this;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time = TOD_Data.main.TimeNow;
        content = "Time: " + time + "\nNumber of Cubes: " + cubeNumber;
        myText.text = content;
    }

    public void OneMoreCube(){
        cubeNumber++;
    }
}
