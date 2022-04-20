using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

public class Customer : MonoBehaviour
{
    Inventory inventory;

    [Header("Customer's Order")]
    //public int shuffle;
    [SerializeField] int minOrder = 3, maxOrder = 7;
    [SerializeField] int minQ = 1, maxQ = 5;
    private bool hasOrder;
    [SerializeField] private TextMeshProUGUI customerOrderText;
    public GameObject customerCanvas;

    //Lookat player variables
    [Header("Lookat Player Rotation")]
    [SerializeField] private GameObject player;
    [SerializeField] private float rotateSpeed;
    private Coroutine LookCoroutine;
    private Coroutine LookAwayCoroutine;
    private Transform customerOriginalTransform;


    //Gaze at variable
    [Header("GazeTime")]
    [SerializeField] private float totalTime = 2;
    bool gvrStatus;
    float gvrTimer;

    [Header("Timer UI")]
    [SerializeField] float totalSeconds;
    private float oriTotalSeconds;
    float elapsedSeconds;
    bool countdownRunning = false;
    [SerializeField] private Image uiFillImage;
    [SerializeField] private TextMeshProUGUI uiText;
    [SerializeField] private GameObject timerObj;

    //Random angry text
    private string[] texts = new string[] { "My grandmother also cook faster than you!!!", 
                                            "I won't visit this place again!!!", 
                                            "Fuck this place,I'm out!!" };

    //List is for customer to store & output their orders
    // Orders are randomly churned. Randomness affects 
    // quantity and ingredient type. 
    // Burger ingredients: bun, sauce, tomatoes, lettuce, cheese, egg, meat
    // Customer sld choose 5 ing minimum, 
    List<string> ingredients = new List<string> { "Bun", "Sauce", "Tomato", "Meat", "Lettuce", "Cheese", "Egg" };
    List<int> iQuantity = new List<int>();

    //Public for checking with inventory
    public List<string> cusOrder;
    private bool isCompleteOrder = false;

    void Awake()
    {
        inventory = Inventory.instance;

        hasOrder = false;
        countdownRunning = false;
        customerCanvas.SetActive(false);
        customerOriginalTransform = this.gameObject.transform;
        ResetTimer();   //initialise countdown timer value
    }

    void Update()
    {

        if (gvrStatus)
        {
            gvrTimer += Time.deltaTime;

            if (gvrTimer >= totalTime)
            {
                customerCanvas.SetActive(true);
                if (!hasOrder)
                {
                    hasOrder = true; 
                    PrintOrder();
                    countdownRunning = true;
                }
            }
        }

        if (countdownRunning)
        {
            timerObj.SetActive(true);
            timerObj.transform.LookAt(player.transform);
            //Display timer UI
            DisplayTime(totalSeconds);

            //countdown timer
            totalSeconds -= Time.deltaTime;
            if (totalSeconds <= elapsedSeconds)
            {
                countdownRunning = false;
                CustomerGetAngry();
            }
        }

        //Check customer order
        if (hasOrder)
        {
            isCompleteOrder = inventory.CheckCustomerOrder(cusOrder);

            if (isCompleteOrder)
            {
                inventory.ClearFoods();
            }
        }
    }
    public void GVROn()
    {
        gvrStatus = true;
        LookatPlayer();     //turn to player

        if (LookAwayCoroutine != null)      //stop look away coroutine
        {
            StopCoroutine(LookAwayCoroutine);
        }   
    }

    public void GVROff()
    {
        gvrStatus = false;
        gvrTimer = 0;
        customerCanvas.SetActive(false);
        StopCoroutine(LookCoroutine);       //stop lookat coroutine
        LookAwayPlayer();       //look away from player

    }

    /// <summary>
    /// Get random items from ingredient list
    /// Transfer all randomized items into customer order list
    /// </summary>
    /// <param name="ingredients"></param>
    /// <param name="count"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    List<string> GetRandomIngredients(List<string> ingredients, int count)
    {
        cusOrder = new List<string>();
        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, ingredients.Count);
            cusOrder.Add(ingredients[index]);
        }
        return cusOrder;
    }

    void PrintOrder()
    {
        // Randomize number of ingredients for burger
        int iCount = Random.Range(minOrder, maxOrder);
        var customerOrder = GetRandomIngredients(ingredients, iCount);
        // Randomize Quantity for ingredients
        int iQuantity = Random.Range(minQ, maxQ);
        Debug.Log("Amount: " + iQuantity);
        Debug.Log("All ingredients -> " + string.Join(", ", ingredients));
        Debug.Log("Customer Orders -> " + string.Join(", ", customerOrder));
        customerOrderText.text = string.Join(",", customerOrder);
    }

    //Rotate custumor and look at player
    private void LookatPlayer()
    {
        if (LookCoroutine != null)
        {
            StopCoroutine(LookCoroutine);
        }

        LookCoroutine = StartCoroutine(facingPlayer());
    }

    private IEnumerator facingPlayer()
    {
        Quaternion lookRotation = Quaternion.LookRotation(player.transform.position - transform.position);

        float time = 0;

        while (time < 1)
        {
            transform.rotation= Quaternion.Slerp(transform.rotation, lookRotation, time);

            time += Time.deltaTime * rotateSpeed;

            yield return null;
        }
    }

    //Turns back to original position after player not looking at customer
    private void LookAwayPlayer()
    {
        if (LookAwayCoroutine != null)
        {
            StopCoroutine(LookAwayCoroutine);
        }

        LookAwayCoroutine = StartCoroutine(LookAwayRotine());
    }

    private IEnumerator LookAwayRotine()
    {
        Quaternion lookRotation = Quaternion.LookRotation(customerOriginalTransform.position - transform.position);

        float time = 0;

        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);

            time += Time.deltaTime * rotateSpeed;

            yield return null;
        }
    }

    //Initialise timer value
    private void ResetTimer()
    {
        elapsedSeconds = 0;
        oriTotalSeconds = totalSeconds;
        timerObj.SetActive(false);
    }

    //Display timer UI
    private void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        uiText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        uiFillImage.fillAmount = totalSeconds / oriTotalSeconds;
    }

    //Display UI when customer gets angry and leave the place
    private void CustomerGetAngry()
    {
        customerCanvas.SetActive(true);
        LookatPlayer();
        string currentText = texts[Random.Range(0, texts.Length)];
        customerOrderText.text = currentText;
        Destroy(gameObject,3f);
    }
}
