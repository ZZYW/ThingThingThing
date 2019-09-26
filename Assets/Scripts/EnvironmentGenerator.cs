using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGenerator : MonoBehaviour {

	public GameObject[] terrains = new GameObject[1];
	private MeshFilter meshFilter;
	private MeshCollider meshCollider;

	[Range (1, 1000)][SerializeField] float divider = 214;
	[Range (0, 1000)][SerializeField] float meshYOffset = 144;
	[Range (0, 100)][SerializeField] float meshYOffsetRandomRange = 50;

	void Awake () {
		foreach (GameObject terrain in terrains) {
			ChangeMesh (terrain);
		}
	}

	void ChangeMesh (GameObject terrain) {
		meshFilter = terrain.GetComponent<MeshFilter> ();
		meshCollider = terrain.GetComponent<MeshCollider> ();

		float rand = Random.Range (10, 100);

		float meshYOffsetFinal = meshYOffset + Random.Range (-meshYOffsetRandomRange, meshYOffsetRandomRange);
		Mesh mesh = meshFilter.mesh;

		Vector3[] vertices = mesh.vertices;
		Color[] colors = new Color[vertices.Length];

		for (int i = 0; i < vertices.Length; i++) {
			Vector3 tempVec = vertices[i];
			float noiseOutput = Mathf.PerlinNoise ((tempVec.x + rand) / divider, (tempVec.z + rand) / divider);
			tempVec.y = (noiseOutput - 0.5f) * meshYOffsetFinal; //Random.Range (-meshYOffset, meshYOffset);
			vertices[i] = tempVec;
		}

		mesh.vertices = vertices;
		mesh.RecalculateNormals ();
		meshCollider.sharedMesh = mesh;
	}

	

	void Start () {

	}

	void Update () {

	}

}