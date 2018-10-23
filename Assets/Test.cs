using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	public List<Renderer> renderers = new List<Renderer> ();

	// Use this for initialization
	void Start () {

		Transform[] gos = FindObjectsOfType<Transform> ();
		foreach (var GO in gos) {
			if (GO.gameObject.GetComponent<Renderer> ()) {
				renderers.Add (GO.gameObject.GetComponent<Renderer> ());

			}
		}

		foreach (var rend in renderers) {
			var mats = rend.materials;
			foreach (var mat in mats) {
				mat.shader = Shader.Find ("Unlit/Color");
			}

		}
	}

	// Update is called once per frame
	void Update () {

	}
}