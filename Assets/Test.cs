using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour {

	public List<Renderer> renderers = new List<Renderer> ();

	public Text text;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		// if( text )
		text.text = Random.Range(10000,1000).ToString();
	}
}