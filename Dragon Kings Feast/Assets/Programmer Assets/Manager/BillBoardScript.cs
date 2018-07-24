using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoardScript : MonoBehaviour
{
    private float timer;


	void Start () {
		
	}
	
	void Update ()
    {
        timer += Time.deltaTime;

        if (timer > 0.25f)
        {
            transform.LookAt(Camera.main.transform.position);
            transform.Rotate(new Vector3(transform.rotation.x + 180.0f, transform.rotation.y, transform.rotation.z));
        }
	}
}
