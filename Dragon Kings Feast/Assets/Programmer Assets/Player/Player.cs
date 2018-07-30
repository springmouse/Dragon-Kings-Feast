using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{    
    //this is used to stop the phone from moving back so suddenly
    [Range(0, 1)]
    public float tiltBackCutOff;
    
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
    public Vector3 startPos;

    public bool inverseCameraTilt;

    public Text debugText;

    public float accelerpmitorDefaultZ;

    private void Awake()
    {
        inverseCameraTilt = false;
        startPos = transform.position;

        accelerpmitorDefaultZ = Input.acceleration.z;
    }

        private void Update()
    {
#if UNITY_ANDROID
        ReadPhoneControls();
#endif

        velocity.x = forwardMoveSpeed;
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

        float xHolder = -Input.acceleration.x;

        acceleration.z = xHolder;

        Debug.Log("running, X = " + acceleration);

        //////////////////Up, down/////////////////

        if (inverseCameraTilt == false)
        {
            float tiltvalue = Input.acceleration.z + tiltBackCutOff;

            Debug.Log("running, X = " + tiltvalue);

            float yHolder = accelerpmitorDefaultZ - tiltvalue;

            acceleration.y = yHolder;
        }
        else
        {
            float tiltvalue = Input.acceleration.z + tiltBackCutOff;

            Debug.Log("running, X = " + tiltvalue);

            float yHolder = accelerpmitorDefaultZ - tiltvalue;

            acceleration.y = -yHolder;
        }

        //acceleration *= 100;
        //acceleration = new Vector3((int)acceleration.x, (int)acceleration.y, (int)acceleration.z);
        //acceleration /= 100;

        velocity = (acceleration);

    }
    
    public void SwapInverse()
    {
        inverseCameraTilt = inverseCameraTilt == true ? false : true;
    }

    public void SetDefaultY()
    {
        accelerpmitorDefaultZ = Input.acceleration.z;
    }
}


