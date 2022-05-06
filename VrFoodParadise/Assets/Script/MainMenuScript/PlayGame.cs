using UnityEngine;
using UnityEngine.UI;

public class PlayGame : MonoBehaviour
{
    //Gaze at variable
    [Header("GazeTime")]
    [SerializeField] private float totalTime = 2;
    bool gvrStatus;
    float gvrTimer;
    [SerializeField] private Image imgGaze;
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject levelSelectionUI;
    private bool canOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        gvrStatus = false;
        levelSelectionUI.SetActive(false);
        mainMenuUI.SetActive(true);
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
                canOpen = true;
                GVROff();
            }
        }

        if (canOpen)
        {
            OpenUI();
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

    private void OpenUI()
    {
        levelSelectionUI.SetActive(true);
        mainMenuUI.SetActive(false);
        canOpen = false;
    }
}
