using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int leftSpeed;
    private float leftTimer;

    public int rightSpeed;
    private float rightTimer;

    public int upSpeed;
    private float upTimer;

    public int downSpeed;
    private float downTimer;


    public int maxHorizontal;
    public int maxVertical;

    public float moveSpeed;
    public float accelerationSpeed;

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
