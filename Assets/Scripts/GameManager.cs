using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public variables
    public Transform startingPoint; //variable for game object in unity
    public float lerpSpeed; //constant speed between 2 points
    public float score; //score variable

    //private variables
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        //finds player object and gets component PlayerController script
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        score = 0; //on game start score set to 0

        playerControllerScript.gameOver = true;
        StartCoroutine(PlayIntro());
    }

    // Update is called once per frame
    void Update()
    {
        //if game over = false
        if (!playerControllerScript.gameOver)
        {
            //if double speed active score increses by 2, else increases by 1
            if (playerControllerScript.doubleSpeed)
            {
                score += 2; //score + 2
            }
            else{
                score++; //score + 1
            }
            Debug.Log("Score: " + score); //console log concatenated string
        }
    }

    IEnumerator PlayIntro()
    {
        Vector3 startPos = playerControllerScript.transform.position; //start position equals player position
        Vector3 endPos = startingPoint.position; //end position is equal to starting point
        float journeyLength = Vector3.Distance(startPos, endPos); //length of journey, distance between startPos and endPos
        float startTime = Time.time; //starting time

        float distanceCovered = (Time.time - startTime) * lerpSpeed; //distance covered equals current time minus startTime multiplied by lerpSpeed
        float fractionOfJourney = distanceCovered / journeyLength; //fraction of journey equals distance covered divided by length of journey

        //get animator component and halve speed multiplier
        playerControllerScript.GetComponent<Animator>().SetFloat("Speed_Multiplier", 0.5f);

        //while loop that plays while fraction of journey variable is less than 1
        while (fractionOfJourney < 1)
        {
            //calculates the fraction of journey value to move player forward
            distanceCovered = (Time.time - startTime) * lerpSpeed;
            fractionOfJourney = distanceCovered / journeyLength;
            //moves player at constant speed between start and end positions
            playerControllerScript.transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);
            yield return null; //break loop once fraction of journey reaches 1
        }

        //after while loop breaks
        playerControllerScript.GetComponent<Animator>().SetFloat("Speed_Multiplier", 1.0f); //return speed multiplier to normal speed
        playerControllerScript.gameOver = false; //game over to false
    }
}
