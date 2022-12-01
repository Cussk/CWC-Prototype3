using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    //private variables
    private Vector3 startPos; //starting position of object
    private float repeatWidth; //width of background offset

    // Start is called before the first frame update
    void Start()
    {
        //starting positoin equals current position of object
        startPos = transform.position;
        //width offset is equal to half the size of the background's box collider on x-axis
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        //if position on x-axis is less than startPos offset by repeatWidth
        if (transform.position.x < startPos.x - repeatWidth)
        {
            //reset postion to start position
            transform.position = startPos;
        }
    }
}
