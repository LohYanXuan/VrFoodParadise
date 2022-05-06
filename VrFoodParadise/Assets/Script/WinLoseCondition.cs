using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseCondition : MonoBehaviour
{
    [SerializeField] private GameObject winOneStar;
    [SerializeField] private GameObject winTwoStar;
    [SerializeField] private GameObject winThreeStar;
    [SerializeField] private GameObject loseUI;
    [SerializeField] private GameObject totalScore;
    private void Start()
    {
        winOneStar.SetActive(false);
        winTwoStar.SetActive(false);
        winThreeStar.SetActive(false);
        loseUI.SetActive(false);
        totalScore.SetActive(false);
    }

    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Customer").Length <=0)
        {
            totalScore.SetActive(true);
            if (Inventory.instance.score >= 100)
            {
                winThreeStar.SetActive(true);
            }
            else if(Inventory.instance.score >= 80 && Inventory.instance.score <= 99)
            {
                winTwoStar.SetActive(true);
            }
            else if (Inventory.instance.score >= 50 && Inventory.instance.score <= 79)
            {
                winOneStar.SetActive(true);
            }
            else
            {
                loseUI.SetActive(true);
            }
            Invoke("OpenMainMenu", 5f);
        }
    }

    private void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
}
