using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TriangleNet.Geometry;
using TriangleNet.Meshing;
using TriangleNet.Voronoi;
using TriangleNet.Topology;

public class TerrainGenerator : MonoBehaviour
{

    [Header("Multi Generation Paras")]
    public int numberOfTerrains = 4;
    [Header("Single Terrain Parameters")]
    public int vertexNumber = 400;
    public Material material;
    // Maximum size of the terrain.
    public int xsize = 100;
    public int ysize = 100;
    // Triangles in each chunk.
    public int trianglesInChunk = 20000;

    [Header("Noise Parameters")]
    public float elevationScaleLow = 50.0f;
    public float elevationScaleHigh = 100.0f;
    public float sampleSize = 1.0f;
    public int octavesLow = 3;
    public int octavesHigh = 10;
    public float frequencyBase = 2;
    public float persistenceLow = 0.4f;
    public float persistenceHigh = 1.1f;

    // Elevations at each point in the mesh
    List<float> elevations = new List<float>();
    // The delaunay mesh
    TriangleNet.Mesh mesh = null;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberOfTerrains; i++)
        {
            MakeMesh();
        }
    }

    public void MakeMesh()
    {
        float elevationScale = Random.Range(elevationScaleLow, elevationScaleHigh);
        Debug.Log(elevationScale);
        int octaves = Random.Range(octavesLow, octavesHigh);
        float persistence = Random.Range(persistenceLow, persistenceHigh);



        // Vertex is TriangleNet.Geometry.Vertex
        Polygon polygon = new Polygon();
        for (int i = 0; i < vertexNumber; i++)
        {
            var newVer = new Vertex(Random.Range(0, xsize / 2f), Random.Range(0, ysize / 2f));
            polygon.Add(newVer);
        }

        // ConformingDelaunay is false by default; this leads to ugly long polygons at the edges
        // because the algorithm will try to keep the mesh convex
        TriangleNet.Meshing.ConstraintOptions options = new TriangleNet.Meshing.ConstraintOptions() { ConformingDelaunay = true };
        mesh = (TriangleNet.Mesh)polygon.Triangulate(options);

        float[] seed = new float[octaves];
        for (int i = 0; i < octaves; i++)
        {
            seed[i] = Random.Range(0.0f, 100.0f);
        }
        // Sample perlin noise at each generated point to get elevation and store it in
        // the elevations array
        foreach (Vertex vert in mesh.Vertices)
        {
            float elevation = 0.0f;
            float amplitude = Mathf.Pow(persistence, octaves);
            float frequency = 1.0f;
            float maxVal = 0.0f;

            for (int o = 0; o < octaves; o++)
            {
                float sample = (Mathf.PerlinNoise(seed[o] + (float)vert.x * sampleSize / (float)xsize * frequency,
                                                  seed[o] + (float)vert.y * sampleSize / (float)ysize * frequency) - 0.5f) * amplitude;
                elevation += sample;
                maxVal += amplitude;
                amplitude /= persistence;
                frequency *= frequencyBase;
            }

            elevation = elevation / maxVal;
            elevations.Add(elevation * elevationScale);
        }

        // Instantiate an enumerator to go over the Triangle.Net triangles - they don't
        // provide any array-like interface for indexing
        IEnumerator<Triangle> triangleEnumerator = mesh.Triangles.GetEnumerator();

        // Create more than one chunk, if necessary
        for (int chunkStart = 0; chunkStart < mesh.Triangles.Count; chunkStart += trianglesInChunk)
        {
            // Vertices in the unity mesh
            List<Vector3> vertices = new List<Vector3>();
            // Per-vertex normals
            List<Vector3> normals = new List<Vector3>();
            // Per-vertex UVs - unused here, but Unity still wants them
            List<Vector2> uvs = new List<Vector2>();
            // Triangles - each triangle is made of three indices in the vertices array
            List<int> triangles = new List<int>();

            // Iterate over all the triangles until we hit the maximum chunk size
            int chunkEnd = chunkStart + trianglesInChunk;
            for (int i = chunkStart; i < chunkEnd; i++)
            {
                if (!triangleEnumerator.MoveNext())
                {
                    // If we hit the last triangle before we hit the end of the chunk, stop
                    break;
                }

                // Get the current triangle
                Triangle triangle = triangleEnumerator.Current;

                // For the triangles to be right-side up, they need
                // to be wound in the opposite direction
                Vector3 v0 = GetPoint3D(triangle.vertices[2].id);
                Vector3 v1 = GetPoint3D(triangle.vertices[1].id);
                Vector3 v2 = GetPoint3D(triangle.vertices[0].id);

                // This triangle is made of the next three vertices to be added
                triangles.Add(vertices.Count);
                triangles.Add(vertices.Count + 1);
                triangles.Add(vertices.Count + 2);

                // Add the vertices
                vertices.Add(v0);
                vertices.Add(v1);
                vertices.Add(v2);

                // Compute the normal - flat shaded, so the vertices all have the same normal
                Vector3 normal = Vector3.Cross(v1 - v0, v2 - v0);
                normals.Add(normal);
                normals.Add(normal);
                normals.Add(normal);

                // If you want to texture your terrain, UVs are important,
                // but I just use a flat color so put in dummy coords
                uvs.Add(new Vector2(0.0f, 0.0f));
                uvs.Add(new Vector2(0.0f, 0.0f));
                uvs.Add(new Vector2(0.0f, 0.0f));
            }

            // Create the actual Unity mesh object
            Mesh chunkMesh = new Mesh();
            chunkMesh.vertices = vertices.ToArray();
            chunkMesh.uv = uvs.ToArray();
            chunkMesh.triangles = triangles.ToArray();
            chunkMesh.normals = normals.ToArray();
            chunkMesh.RecalculateNormals();
            chunkMesh.RecalculateTangents();
            chunkMesh.RecalculateBounds();

            // Instantiate the GameObject which will display this chunk
            Transform chunk = new GameObject("terrain patch").GetComponent<Transform>();
            chunk.gameObject.AddComponent<MeshFilter>().mesh = chunkMesh;
            chunk.gameObject.AddComponent<MeshCollider>().sharedMesh = chunkMesh;
            var rend = chunk.gameObject.AddComponent<MeshRenderer>();
            rend.sharedMaterial = material;
            // rend.material = ColorManager.instance.GetMaterial(1f, false,false);
            chunk.gameObject.AddComponent<MeshCollider>();
            chunk.transform.parent = transform;
            chunk.transform.position = new Vector3(-xsize / 4f, transform.position.y, -ysize / 4f);
            // chunk.transform.Translate(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
        }
    }

    // This method returns the world-space vertex for a given vertex index
    public Vector3 GetPoint3D(int index)
    {
        Vertex vertex = mesh.vertices[index];
        float elevation = elevations[index];
        return new Vector3((float)vertex.x, elevation, (float)vertex.y);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
