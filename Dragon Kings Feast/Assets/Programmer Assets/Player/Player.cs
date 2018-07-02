using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //these affect your max speed in a certin direction
    public int rightSpeed;
    public int leftSpeed;
    public int upSpeed;
    public int downSpeed;

    //these values are used to manage the acceleration in a direction
    //programmer use only
    private float rightTimer;
    private float leftTimer;
    private float upTimer;
    private float downTimer;

    //these vlause are used to dictate how far to the sides and up and down a player can go
    //assume that what ever value you put in is mirroed both in the positive and negative
    //eg. maxHorizontal = 10 means the player can only move between -10 and 10 on the z plane
    public int maxHorizontal;
    public int maxVertical;

    //Move speed affects the global max speed, so if over all you feel that everything is to slow or 
    //everything needs to speed up change this value recomanded min of 1
    public float moveSpeed;
    //this affects how fast the player reaches there maxs speed so with an acceleration speed of 1 you will reach your max speed in 1 seconds
    //with an acceleration speed of 2 you will reach your max speed in 0.5 seconds
    public float accelerationSpeed;

    //this shows the players current velocity, you should never need to change it directly it is more of a refrence
    public Vector3 velocity;

    private void Update()
    {
        ReadControls();

        transform.position += (velocity * Time.deltaTime) * moveSpeed;

        LockPos();
    }

    private void LockPos()
    {
        //////////////////Right/////////////////

        if (-maxHorizontal > transform.position.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -maxHorizontal);
        }

        //////////////////Left/////////////////

        if (maxHorizontal < transform.position.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, maxHorizontal);
        }

        //////////////////Up/////////////////

        if (maxVertical < transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, maxVertical, transform.position.z);
        }

        //////////////////Down/////////////////

        if (-maxVertical > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, -maxVertical, transform.position.z);
        }
    }

    private void ReadControls()
    {
        Vector3 acceleration = new Vector3(0,0,0);

        //////////////////Right/////////////////

        if (Input.GetKey(KeyCode.D))
        {
            acceleration.z += Mathf.Lerp(0, rightSpeed, rightTimer);

            if (rightTimer < 1.0f)
            {
                rightTimer += Time.deltaTime * accelerationSpeed;

                if (rightTimer > 1.0f)
                {
                    rightTimer = 1.0f;
                }
            }
        }

        if (Input.GetKey(KeyCode.D) == false)
        {
            acceleration.z += Mathf.Lerp(0, rightSpeed, rightTimer);

            if (rightTimer > 0.0f)
            {
                rightTimer -= Time.deltaTime * accelerationSpeed;

                if (rightTimer < 0.0f)
                {
                    rightTimer = 0.0f;
                }
            }
        }

        //////////////////Left/////////////////

        if (Input.GetKey(KeyCode.A))
        {
            acceleration.z += Mathf.Lerp(0, leftSpeed, leftTimer);

            if (leftTimer < 1.0f)
            {
                leftTimer += Time.deltaTime * accelerationSpeed;

                if (leftTimer > 1.0f)
                {
                    leftTimer = 1.0f;
                }
            }
        }

        if (Input.GetKey(KeyCode.A) == false)
        {
            acceleration.z += Mathf.Lerp(0, leftSpeed, leftTimer);

            if (leftTimer > 0.0f)
            {
                leftTimer -= Time.deltaTime * accelerationSpeed;

                if (leftTimer < 0.0f)
                {
                    leftTimer = 0.0f;
                }
            }
        }

        //////////////////Up/////////////////

        if (Input.GetKey(KeyCode.W))
        {
            acceleration.y += Mathf.Lerp(0, upSpeed, upTimer);

            if (upTimer < 1.0f)
            {
                upTimer += Time.deltaTime * accelerationSpeed;

                if (upTimer > 1.0f)
                {
                    upTimer = 1.0f;
                }
            }
        }

        if (Input.GetKey(KeyCode.W) == false)
        {
            acceleration.y += Mathf.Lerp(0, upSpeed, upTimer);

            if (upTimer > 0.0f)
            {
                upTimer -= Time.deltaTime * accelerationSpeed;

                if (upTimer < 0.0f)
                {
                    upTimer = 0.0f;
                }
            }
        }

        //////////////////Down/////////////////

        if (Input.GetKey(KeyCode.S))
        {
            acceleration.y += Mathf.Lerp(0, downSpeed, downTimer);
            
            if (downTimer < 1.0f)
            {
                downTimer += Time.deltaTime * accelerationSpeed;

                if (downTimer > 1.0f)
                {
                    downTimer = 1.0f;
                }
            }
        }

        if (Input.GetKey(KeyCode.S) == false)
        {
            acceleration.y += Mathf.Lerp(0, downSpeed, downTimer);

            if (downTimer > 0.0f)
            {
                downTimer -= Time.deltaTime * accelerationSpeed;

                if (downTimer < 0.0f)
                {
                    downTimer = 0.0f;
                }
            }
        }

        velocity = acceleration;
        
    }
}
