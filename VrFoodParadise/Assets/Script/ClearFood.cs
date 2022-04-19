using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearFood : MonoBehaviour
{
    Inventory inventory;

    [Header("ObjectSettings")]
    private Renderer myRenderer;
    Material oriMat;
    public Material gazeMat;

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

        if (GetComponent<Renderer>() != null)
        {
            myRenderer = GetComponent<Renderer>();
            oriMat = myRenderer.material;
        }

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
            imgGaze.fillAmount = gvrTimer / 3f;
        }

        OutlineWhenGaze();

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

                if (timer - startTime >= 3)
                {
                    isGaze = false;
                    startTime = 0;
                    timer = 0;
                    isResetTimer = false;

                    //gvrStatus = false;
                    gvrTimer = 0;
                    imgGaze.fillAmount = 0;

                    //Clear the food(s) in inventory if player make mistake on the ingredient and wish to retry
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

    private void OutlineWhenGaze()
    {
        if (gazeMat != null)
        {
            if (isGaze)
            {
                myRenderer.material = gazeMat;
            }
            else
            {
                myRenderer.material = oriMat;
            }
        }
    }

}
