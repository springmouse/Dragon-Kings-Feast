using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text fpsText;

    float timer;
    
    public int currentFPS;

	void Start ()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
	
	void Update ()
    {
        timer += Time.deltaTime;

        if (timer >= 1.0f)
        {
            timer = 0;
            fpsText.text = (currentFPS).ToString();
            currentFPS = 0;
        }

        currentFPS++;
	}
}
