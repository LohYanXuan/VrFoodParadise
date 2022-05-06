using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitGame : MonoBehaviour
{
    //Gaze at variable
    [Header("GazeTime")]
    [SerializeField] private float totalTime = 2;
    bool gvrStatus;
    float gvrTimer;
    [SerializeField] private Image imgGaze;

    // Start is called before the first frame update
    void Start()
    {
        gvrStatus = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gvrStatus)
        {
            gvrTimer += Time.deltaTime;
            imgGaze.fillAmount = gvrTimer / totalTime;
            if (gvrTimer >= totalTime)
            {
                Application.Quit();
            }
        }
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

}
