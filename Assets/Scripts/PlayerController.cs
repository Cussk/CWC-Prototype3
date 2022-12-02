using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public variables
    public float jumpForce = 10; //variable how high jump is
    public float gravityModifier; //variable adjust level of gravity
    public float doubleJumpForce; //how high second jump is
    public bool doubleJumpUsed = false; //double jump used true or false
    public bool isOnGround = true; //variable checks is player is on the ground
    public bool gameOver = false; //gameover true or false
    public bool doubleSpeed = false; //double speed used true or false 
    public ParticleSystem explosionParticle; //variable to set particle type
    public ParticleSystem dirtParticle; //variable to set particle type
    public AudioClip jumpSound; //variable to set audio clips
    public AudioClip crashSound; //variable to set audio clips

    //private variables
    private Rigidbody playerRB; //variable for rigidbody component
    private Animator playerAnim; //variable for animator component
    private AudioSource playerAudio; //variable for audiosource component

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>(); //gets rigidbody component from player object
        playerAnim = GetComponent<Animator>(); //gets animator component
        playerAudio = GetComponent<AudioSource>(); //get Audio Source component
        Physics.gravity *= gravityModifier; //physics for gravity is equal to gravity * gravitiyModifier
        
    }

    // Update is called once per frame
    void Update()
    {
        //player presses spacebar AND on ground will jump AND game not over
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); //enacts physics on rigidbody component, impulse enacts force immediately rather than over time
            isOnGround = false; //player not on ground 
            playerAnim.SetTrigger("Jump_trig"); //activates jump animation via jump trigger in animation tree
            dirtParticle.Stop(); //stops particle effect
            playerAudio.PlayOneShot(jumpSound, 1.0f); //play sound effect once at full volume
            doubleJumpUsed = false; //double jump not used yet
        }
        //player press space AND not on ground AND not used second jump, perform double jump
        else if (Input.GetKeyDown(KeyCode.Space) && !isOnGround && !doubleJumpUsed)
        {
            doubleJumpUsed = true; //changes double jump to true
            playerRB.AddForce(Vector3.up * doubleJumpForce, ForceMode.Impulse); //physics for second jump
            playerAnim.Play("Running_Jump", 3, 0f); //resets jump animation to first frame
            playerAudio.PlayOneShot(jumpSound, 1.0f); //play sound effect once at full volume
        }

        //when player holds shift doubles player speed
        if(Input.GetKey(KeyCode.LeftShift))
        {
            doubleSpeed = true; //set double speed to true
            playerAnim.SetFloat("Speed_Multiplier", 2.0f); //multiplies player animation speed
        }
        //when player is not holding shift double speed turns off
        else if(doubleSpeed)
        {
            doubleSpeed = false; //sets double speed to false
            playerAnim.SetFloat("Speed_Multiplier", 1.0f); //sets player animation speed to normal
        }
    }

    //checks collisions with player
    private void OnCollisionEnter(Collision collision) 
    {
        //if player collides with ground switch isOnGround back to true 
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true; //changes isOnGround to true
            dirtParticle.Play(); //plays particle effect
        } 
        //if player collides with obstacle game over
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;//changes gameOver to true
            Debug.Log("Game Over"); //logs Game Over to console
            playerAnim.SetBool("Death_b", true); //activates death animation
            playerAnim.SetInteger("DeathType_int", 1); //sets animation in animation tree
            explosionParticle.Play(); //plays particle effect
            dirtParticle.Stop(); //stops particle effect
            playerAudio.PlayOneShot(crashSound, 1.0f); //play sound effect once at full volume
        }
    }
}
