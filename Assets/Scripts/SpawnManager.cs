using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //public variables
    public GameObject obstaclePrefab;

    //private variables
    private Vector3 spawnPos = new Vector3(25, 0, 0); //spawn position x-axis 25, y 0, z 0
    private float startDelay = 2;
    private float repeatRate = 2;

    // Start is called before the first frame update
    void Start()
    {
        //repeated calls SPawObstacle at set intervals
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //spawn obstacle when called
    void SpawnObstacle()
    {
        //spawns obstacle
        Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
    }
}
