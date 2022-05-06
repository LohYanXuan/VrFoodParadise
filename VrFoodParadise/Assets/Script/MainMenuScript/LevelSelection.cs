using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    //Gaze at variable
    [Header("GazeTime")]
    [SerializeField] private float totalTime = 2;
    bool gvrStatus;
    float gvrTimer;
    [SerializeField] private Image imgGaze;
    public bool level1 = false;
    public bool level2 = false;
    public bool level3 = false;
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
                if (level1)
                {
                    SceneManager.LoadScene("Level_1");
                }
                else if (level2)
                {
                    SceneManager.LoadScene("Level_2");
                }
                else if (level3)
                {
                    SceneManager.LoadScene("Level_3");
                }
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
