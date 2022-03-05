using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float totalSeconds = 0; 
    float elapsedSeconds = 0; 
    bool running = false; 
    bool started = false; 

    // Set duration of the timer
    // Can only be set if timer isn't running
    public float Duration
    {
        set 
        {
            if (!running)
            {
                totalSeconds = value; 
            }
        }
    }

    // Gets whether or not the timer has finished running
    public bool Finished
    {
		get { return started && !running; } 
	}

	// Gets whether or not the timer is currently running
    public bool Running
    {
		get { return running; }
	}

    public void Run()
    {
        if (totalSeconds > 0)
        {
            started = true; 
            running = true; 
            elapsedSeconds = 0; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            elapsedSeconds += Time.deltaTime; 
            if(elapsedSeconds >= totalSeconds)
            {
                running = false; 
            }
        }
    }
}
