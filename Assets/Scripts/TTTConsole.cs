using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;

public class TTTConsole : MonoBehaviour
{

    public static TTTConsole A;


    public Text consoleText;
    public ScrollRect scrollRect;
    public int maxLength = 2000;

    [Range(0, 1)]
    public float verticalNormalizedPosition;


    StringBuilder stringBuilder = new StringBuilder();


    public bool test;

    //reference
    private TextAsset log;


    private void Awake()
    {
        A = this;
    }

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        scrollRect.verticalNormalizedPosition = verticalNormalizedPosition;
    }

    public void Log(string content)
    {

        stringBuilder.Append(content);
        consoleText.text = stringBuilder.ToString();

        if (stringBuilder.Length > maxLength)
        {
            stringBuilder.Remove(0, stringBuilder.Length - maxLength);
        }

    }
}
