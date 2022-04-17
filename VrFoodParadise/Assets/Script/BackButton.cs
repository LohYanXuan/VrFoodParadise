using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    [SerializeField] private Collider storeCollider;
    [SerializeField] private GameObject menuPanel;

    private float startTime;
    private float timer;
    bool isResetTimer;

    private bool isGaze;

    public void SetGazeAt(bool gazeAt)
    {
        isGaze = gazeAt;
    }

    void Start()
    {
        startTime = 0;
        timer = 0;

        isResetTimer = false;
        isGaze = false;
    }

    void Update()
    {
        if (isGaze)
        {
            if (!isResetTimer)
            {
                startTime = Time.time;
                timer = Time.time;
                isResetTimer = true;
            }
            else
            {
                timer += Time.deltaTime;

                if (timer - startTime >= 2)
                {
                    //Reset timer for keep gazing at same thing
                    isGaze = false;
                    startTime = 0;
                    timer = 0;
                    isResetTimer = false;

                    storeCollider.enabled = true;
                    menuPanel.SetActive(false);
                }
            }
        }
        else
        {
            startTime = 0;
            timer = 0;
            isResetTimer = false;
        }
    }
}
