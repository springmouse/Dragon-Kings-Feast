using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{    
    //this is used to stop the phone from moving back so suddenly
    [Range(0, 1)]
    public float tiltBackCutOff;

    //we use this to speed up the  tilt ()
    public AnimationCurve ForwardTiltSpeed;

    //these vlause are used to dictate how far to the sides and up and down a player can go
    //assume that what ever value you put in is mirroed both in the positive and negative
    //eg. maxHorizontal = 10 means the player can only move between -10 and 10 on the z plane
    public int maxHorizontal;
    public int maxVertical;

    //Move speed affects the global max speed, so if over all you feel that everything is to slow or 
    //everything needs to speed up change this value recomanded min of 1
    public float moveSpeed;

    //this shows the players current velocity, you should never need to change it directly it is more of a refrence
    public Vector3 velocity;
    
    //the speed that the player moves forward
    public float forwardMoveSpeed;
    //the starting position of the charcter in the world
    private Vector3 startPos;

    public Text debugText;

    private void Start()
    {
        startPos = transform.position;
    }

        private void Update()
    {
#if UNITY_ANDROID
        ReadPhoneControls();
#endif
        
        transform.position += (velocity * Time.deltaTime) * moveSpeed;

        LockPos();
    }

    private void LockPos()
    {
        //////////////////Right/////////////////

        if (-maxHorizontal + startPos.z > transform.position.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -maxHorizontal + startPos.z);
        }

        //////////////////Left/////////////////

        if (maxHorizontal + startPos.z < transform.position.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, maxHorizontal + startPos.z);
        }

        //////////////////Up/////////////////

        if (maxVertical + startPos.y < transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, maxVertical + startPos.y, transform.position.z);
        }

        //////////////////Down/////////////////

        if (-maxVertical + startPos.y > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, -maxVertical + startPos.y, transform.position.z);
        }
    }
    
    private void ReadPhoneControls()
    {
        Vector3 acceleration = new Vector3(0, 0, 0);


        //////////////////Right, Left/////////////////

        if (Input.acceleration.x > 0)
        {
            acceleration.z += -Input.acceleration.x;
        }

        if (Input.acceleration.x < 0)
        {
            acceleration.z += -Input.acceleration.x;
        }

        //////////////////Up, down/////////////////

        if (Input.acceleration.z + tiltBackCutOff > (0 ))
        {
            float tiltvalue = Input.acceleration.z + tiltBackCutOff;

            if (tiltBackCutOff > 1)
            {
                tiltBackCutOff = 1;
            }
            
            float holder = tiltvalue * ForwardTiltSpeed.Evaluate(tiltvalue);
                        
            acceleration.y += -holder;
        }

        if (Input.acceleration.z < 0)
        {
            float holder = Input.acceleration.z + tiltBackCutOff;
            
            if (holder > 0)
            {
                holder = 0;
            }
            
            acceleration.y += -holder;
        }

        //acceleration *= 100;
        //acceleration = new Vector3((int)acceleration.x, (int)acceleration.y, (int)acceleration.z);
        //acceleration /= 100;

        velocity = (acceleration);

    }

}


