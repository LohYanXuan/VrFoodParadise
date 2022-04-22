using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckOutButton : MonoBehaviour
{
    Inventory inventory;
    [SerializeField] private GameObject foodForThisStore;
    private GameObject food;

    [SerializeField] private Collider storeCollider;
    [SerializeField] private GameObject menuPanel;

    private float startTime;
    private float timer;
    bool isResetTimer;

    private bool isGaze;

    [Header("LoadingUI")]
    bool gvrStatus;
    float gvrTimer;
    [SerializeField] private Image imgGaze;

    public GameObject[] gazeMenuScript;
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

    void Start()
    {
        inventory = Inventory.instance;

        startTime = 0;
        timer = 0;
        gvrTimer = 0;

        isResetTimer = false;
        isGaze = false;
        gvrStatus = false;
        gazeMenuScript = GameObject.FindGameObjectsWithTag("OrderNumber");
    }

    void Update()
    {
        if (gvrStatus)
        {
            //Fill loading UI
            gvrTimer += Time.deltaTime;
            imgGaze.fillAmount = gvrTimer / 2f;
        }

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

                if (timer - startTime >= 2)
                {
                    //Reset timer for keep gazing at same thing
                    isGaze = false;
                    startTime = 0;
                    timer = 0;
                    isResetTimer = false;

                    gvrStatus = false;
                    gvrTimer = 0;
                    imgGaze.fillAmount = 0;

                    AssignRecipe();
                    menuPanel.SetActive(false);
                    storeCollider.enabled = true;
                    ClearAllIngredentCount();
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

    private void AssignRecipe()
    {
        //Instantiate new gameObject to avoid list being overwrite by instantiating new list
        food = (GameObject)Instantiate(foodForThisStore);

        FoodRecipe foodRecipe = food.GetComponent<FoodRecipe>();

        for (int i = 0; i < inventory.ingredients.Count; i++)
        {
            //Let "food" has a copy of the list of all the ingredients
            foodRecipe.ingredientInIt.Add(inventory.ingredients[i].GetComponent<GazeMenu>().tagName.ToString());
        }

        inventory.InsertFoods(food);
        inventory.ClearIngredients();
    }

    private void ClearAllIngredentCount()
    {
        foreach (GameObject amount in gazeMenuScript)
        {
            GazeMenu temp = amount.GetComponent<GazeMenu>();
            temp.ingredentCount = 0;
        }
    }
}
