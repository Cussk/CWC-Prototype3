using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //public variables
    public GameObject[] obstaclePrefabs; //obstacle array

    //private variables
    private float startDelay = 2;
    private float repeatRate = 2;
    private int randomObstacle;
    private Vector3 spawnPos = new Vector3(25, 0, 0); //spawn position x-axis 25, y 0, z 0
    private PlayerController playerControllerScript; //references PlayerController script

    // Start is called before the first frame update
    void Start()
    {
        //repeated calls SPawObstacle at set intervals
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        //finds player object and gets component PlayerController script
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //spawn obstacle when called
    void SpawnObstacle()
    {
        //if gameOver variable false, spawn obstacles
        if(!playerControllerScript.gameOver)
        {
            //random index
            randomObstacle = Random.Range(0, obstaclePrefabs.Length);
            //spawns random obstacle
            Instantiate(obstaclePrefabs[randomObstacle], spawnPos, obstaclePrefabs[randomObstacle].transform.rotation);
        }
    }
}
