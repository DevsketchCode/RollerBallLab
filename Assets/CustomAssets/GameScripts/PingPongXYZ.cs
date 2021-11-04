using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongXYZ : MonoBehaviour
{

    // Serialized Fields allow the Unity editor to set the value of the property
    [SerializeField]
    private float speed = 0;

    // How far go travel, using a Vector3 allows multiple directions of movement to 
    // make script useable on multiple assets.
    [SerializeField]
    private Vector3 distance = Vector3.zero; // sets xyz all to 0 in one step, allows the designer to also determine the direction.

    // Allows platform to be auto on or triggered on when player lands on it.
    [SerializeField]
    private bool playOnStart = false;

    //Allows platform to be a one-way trip or go back and forth
    [SerializeField]
    private bool loop = false;

    // Time for the platform to wait at the ends to allow player to get on/off, the pause time of the movement. 
    [SerializeField]
    private float pauseTimeAtStart = 0;

    [SerializeField]
    private float pauseTimeAtEnd = 0;


    private Vector3 origPos;
    private Vector3 targetPos;
    private float curDist;
    private float moveSpeed;
    private bool posDir; // positive direction
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        // Determine the start position
        origPos = transform.localPosition; // takes wherever that object is, save it in the variable.

        // Determine the end position based on the distance the designer set.
        targetPos = origPos + distance;

        // moveSpeed, in this case, is the percentage to move the platform each second. 
        // This script will use Lerp()  which is percentage based. 
        moveSpeed = speed / Vector3.Distance(origPos, targetPos); // Vector3.Distance gets the distance between the start and end point

        posDir = true;
        curDist = 0;
        timer = pauseTimeAtStart;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playOnStart) return;  // if not wanting to move the platform yet, then don't move anything or run the statements below.


        if (timer >= 0.0f)
        {
            timer -= Time.deltaTime;
            return; // ends the execution of the method and returns from update, it doesn't do any code after the return.
        }

        MoveObject(); // move the platform

        // Get the location information for the object
        //Vector3 pos = transform.position;

        //pos.x += 0.25f; // every frame it will be 0.25 meters towards the positive direction;
        //pos.x += speed * Time.deltaTime; // this is now time based

        // lerp linear interpolation allows the end point

        //transform.position = pos; // save the variable back to the position, because you can't modify it directly, only through a variable. pull it, modify, set it.
    }

    private void MoveObject()
    {
        // Calculate the distance to move based on how fast each frame calculates
        // this causes the platform to move at a consistent speed regardless of 
        // the Frames Per Second, so slower / faster computers run the same
        if (posDir)
        {
            curDist += moveSpeed * Time.deltaTime;
        }
        else
        {
            curDist -= moveSpeed * Time.deltaTime;
        }

        //curDist += speed * Time.deltaTime;  // this will cause the speed to change on longer objects than shorter objects. 


        // Lerp() uses a percentage to determine position between two points .
        // 0% is at the start point (origPos) and 100% is at the end point (targetPos)
        transform.localPosition = Vector3.Lerp(origPos, targetPos, curDist);

        if (posDir)
        {
            if (curDist >= 1.0f)  // curDist = 1.000001 due to the deltaTime, it will still be caught
            {
                if (loop)
                {
                    posDir = false;
                    timer = pauseTimeAtEnd;
                }
                else
                {
                    // One way trip
                    playOnStart = true;
                    loop = false;
                }
            }
        }
        else
        {
            if (curDist <= 0.0f)
            {
                if (loop)
                {
                    posDir = true;
                    timer = pauseTimeAtStart;
                }
                else
                {
                    // One way trip
                    playOnStart = true;
                    loop = false;
                }
            }
        }


    }

    // check the "is trigger" on the box collider, it makes it a motion sensor.
    // this would be a second box collider put on the object. the first box collider makes the object solid.

    // (OnCollisionEnter is good if you want the trigger but also the collider, that way you don't have to add a second collider.)
    // Be careful with OnTriggerStay, like Update, it is run on EVERY frame that the object is in the trigger area. 
    // Use for OnTriggerStay, can be used to get new position of object that moves through the motion zone and follow it. 

    private void OnTriggerEnter(Collider other)
    {
        // Player landed on the platform, start movement
        if (!playOnStart && other.CompareTag("Player"))
        {
            playOnStart = true;
        }

        /*        if(other.gameObject == Player)
                {
                    Player.transform.parent = transform; // if the platform moves the player will move as well.
                }*/
    }

}
