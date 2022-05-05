using UnityEngine;
using UnityEngine.UI;
public class ResumeButton : MonoBehaviour
{
    //Gaze at variable
    [Header("GazeTime")]
    [SerializeField] private float totalTime = 2;
    bool gvrStatus;
    float gvrTimer;
    [SerializeField] private Image imgGaze;

    [Header("Pause Menu")]
    [SerializeField] private GameObject pauseUI;

    void Update()
    {
        if (gvrStatus)
        {
            gvrTimer += Time.unscaledDeltaTime;
            imgGaze.fillAmount = gvrTimer / totalTime;
            if (gvrTimer >= totalTime)
            {
                ResumeGame();
                GVROff();
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

    private void PauseGame()
    {
        Time.timeScale = 0;
        pauseUI.SetActive(true);
    }
    private void ResumeGame()
    {
        Time.timeScale = 1;
        pauseUI.SetActive(false);
    }
}
