using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    //private variables
    private float speed = 30;
    private PlayerController playerControllerScript; //references PlayerController script
    private float leftBound = -10;

    // Start is called before the first frame update
    void Start()
    {
        //finds player object and gets component PlayerController script
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //if gameOver varaible from PlayerController script is false, move scene left
        if (playerControllerScript.gameOver == false)
        {
        transform.Translate(Vector3.left * Time.deltaTime * speed); //move object left along x-axis
        }

        //if object is less than leftBound AND is an Obstacle object
        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject); //destroy object
        }
    }
}
