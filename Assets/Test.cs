using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		ThingConsole.Log (RandomString(30));
	}

	private static System.Random random = new System.Random ();
	public static string RandomString (int length) {
		const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
		return new string (Enumerable.Repeat (chars, length)
			.Select (s => s[random.Next (s.Length)]).ToArray ());
	}

}