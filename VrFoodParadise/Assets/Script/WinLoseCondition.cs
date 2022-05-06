using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseCondition : MonoBehaviour
{
    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject loseUI;

    private void Start()
    {
        winUI.SetActive(false);
        loseUI.SetActive(false);
    }

    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Customer").Length <=0)
        {
            if (Inventory.instance.score >= 80)
            {
                winUI.SetActive(true);
            }
            else
            {
                loseUI.SetActive(true);
            }
            Invoke("OpenMainMenu", 3f);
        }
    }

    private void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
}
