using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_SpawnObject : MonoBehaviour
{

    public GameObject SpawnableObject;
    public Vector3 spawnPosition;

    // Use this for initialization
    void Start()
    {
        if (SpawnableObject != null)
        {
            Instantiate(SpawnableObject, spawnPosition, Quaternion.identity);
        }
    }

}
