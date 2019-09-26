using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager main;

    public MeshFilter cubeMesh;
    public GameObject sparkPSPrefab;
    public AudioClip sound;

    private void Awake()
    {
        main = this;
    }



}
