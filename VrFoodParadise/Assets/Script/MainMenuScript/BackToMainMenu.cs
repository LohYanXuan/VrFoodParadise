using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class BackToMainMenu : MonoBehaviour
{
    //Gaze at variable
    [Header("GazeTime")]
    [SerializeField] private float totalTime = 2;
    bool gvrStatus;
    float gvrTimer;
    [SerializeField] private Image imgGaze;
    void Update()
    {
        if (gvrStatus)
        {
            gvrTimer += Time.unscaledDeltaTime;
            imgGaze.fillAmount = gvrTimer / totalTime;
            if (gvrTimer >= totalTime)
            {
                ResumeGame();
                SceneManager.LoadScene("MainMenu");
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

    private void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
