using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [Header("Pause Menu")]
    [SerializeField] private GameObject pauseUI;
    private bool isOpen;


    // Start is called before the first frame update
    void Start()
    {
        pauseUI.SetActive(false);
        isOpen = false;
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
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
        pauseUI.SetActive(true);
        isOpen = true;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseUI.SetActive(false);
        isOpen = false;
    }
}
