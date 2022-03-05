using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A script to run the timer class
/// </summary>
public class CDTimer : MonoBehaviour
{
    Timer timer;
    float startTime;

    void Start()
    {
        // create and run timer
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = 3;
        timer.Run();

        // save start time
        startTime = Time.time;
    }

    void Update()
    {
        if (timer.Finished)
        {
            float elapsedTime = Time.time - startTime;

            // Call whatever method here
            Debug.Log("Timer ran for " + elapsedTime + " seconds");

            // save start time and restart timer
            startTime = Time.time;
            timer.Run();
        }
    }
}
