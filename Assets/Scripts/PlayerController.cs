using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10; //variable how high jump is
    public float gravityModifier; //variable adjust level of gravity
    public bool isOnGround = true; //variable checks is player is on the ground
    public bool gameOver = false; //gameover true or false

    private Rigidbody playerRB; //variable for rigidbody component

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>(); //gets rigidbody component from player object
        Physics.gravity *= gravityModifier; //physics for gravity is equal to gravity * gravitiyModifier
    }

    // Update is called once per frame
    void Update()
    {
        //player presses spacebar and on ground will jump
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); //enacts physics on rigidbody component, impulse enacts force immediately rather than over time
            isOnGround = false; //player not on ground 
        }
    }

    //checks collisions with player
    private void OnCollisionEnter(Collision collision) 
    {
        //if player collides with ground switch isOnGround back to true 
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true; //changes isOnGround to true
        } 
        //if player collides with obstacle game over
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;//changes gameOver to true
            Debug.Log("Game Over"); //logs Game Over to console
        }
    }
}
