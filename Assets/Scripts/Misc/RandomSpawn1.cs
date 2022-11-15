using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn1 : MonoBehaviour
{
    public Transform Collectables;
    public Transform[] spawnPoints;

    void Start()
    {
        int indexNumber = Random.Range(0, spawnPoints.Length);
        Collectables.position = spawnPoints[indexNumber].position;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
