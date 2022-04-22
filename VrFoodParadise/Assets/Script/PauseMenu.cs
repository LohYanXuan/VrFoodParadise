using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [Header("Game Score")]
    public int totalScore = 0;

    [Header("Pause Menu")]
    public GvrPointerInputModule gvrInputScript;
    [SerializeField] private GameObject pauseUI;
    private bool isOpen;


    // Start is called before the first frame update
    void Start()
    {
        pauseUI.SetActive(false);
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Option"))
        {
            if (isOpen == false)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }

        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        gvrInputScript.enabled = false;
        pauseUI.SetActive(true);
        isOpen = true;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        gvrInputScript.enabled = true;
        pauseUI.SetActive(false);
        isOpen = false;
    }
}
