using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearFood : MonoBehaviour
{
    Inventory inventory;

    private float startTime;
    private float timer;
    private bool isResetTimer;

    [Header("LoadingUI")]
    bool gvrStatus;
    float gvrTimer;
    public Image imgGaze;

    private bool isGaze;

    void Start()
    {
        inventory = Inventory.instance;

        isGaze = false;
    }

    public void SetGazeAt(bool gazeAt)
    {
        isGaze = gazeAt;
    }

    public void GVROn()
    {
        gvrStatus = true;
    }

    public void GVROff()
    {
        gvrStatus = false;
        gvrTimer = 0;
        imgGaze.fillAmount = 0;
    }

    void Update()
    {
        if (gvrStatus)
        {
            //Fill loading UI
            gvrTimer += Time.deltaTime;
            imgGaze.fillAmount = gvrTimer / 2f;
        }

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
                    isGaze = false;
                    startTime = 0;
                    timer = 0;
                    isResetTimer = false;

                    //gvrStatus = false;
                    gvrTimer = 0;
                    imgGaze.fillAmount = 0;

                    inventory.ClearFoods();
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
