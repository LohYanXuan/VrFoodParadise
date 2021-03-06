using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

public class Customer : MonoBehaviour
{
    //Inventory inventory;

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
    [SerializeField] private Image imgGaze;

    [Header("Timer UI")]
    [SerializeField] float totalSeconds;
    private float oriTotalSeconds;
    float elapsedSeconds;
    bool countdownRunning = false;
    [SerializeField] private Image uiFillImage;
    [SerializeField] private TextMeshProUGUI uiText;
    [SerializeField] private GameObject timerObj;

    public int addPoint;

    //Random angry text
    private string[] angryTexts = new string[] { "My grandmother also cook faster than you!!!", 
                                                 "I won't visit this place again!!!", 
                                                 "Fuck this place,I'm out!!",
                                                 "Never met such noob player like you before..."};
    //Random satisfy, happy text
    private string[] happyTexts = new string[] {"Thank you <3",
                                                "You are so smart,I like it.",
                                                "OMG!!Albert Einstein is still alive. He is playing VR Food Paradise.",
                                                "Well done. Nanti give you $100 extra tips."};

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
        //inventory = Inventory.instance;

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
            imgGaze.fillAmount = gvrTimer / totalTime;
            if (gvrTimer >= totalTime)
            {
                
                if (!hasOrder)
                {    
                    PrintOrder();
                    countdownRunning = true;
                    hasOrder = true;
                }
                else                //Check customer order
                {
                    isCompleteOrder = Inventory.instance.CheckCustomerOrder(cusOrder);

                    if (isCompleteOrder)
                    {
                        //Inventory.instance.ClearFoods();
                        StorePoint();
                        CustomerSatisfy();
                    }
                }
                customerCanvas.SetActive(true);
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
        imgGaze.fillAmount = 0;

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
            int index;
            if (i == 0)
            {
                //Bun at first (top layer)
                index = 0;
            }
            else if (i == count - 1)
            {
                //Bun at last (bottom layer)
                index = 0;
            }
            else
            {
                //Anything in the middle
                index = Random.Range(0, ingredients.Count);
            }
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
        string currentText = angryTexts[Random.Range(0, angryTexts.Length)];
        customerOrderText.text = currentText;
        Destroy(gameObject,3f);
    }

    //Display UI when customer gets thier order correctly and leave the place
    private void CustomerSatisfy()
    {
        //customerCanvas.SetActive(true);
        string currentText = happyTexts[Random.Range(0, happyTexts.Length)];
        customerOrderText.text = currentText;
        Destroy(gameObject, 3f);
    }

    public void StorePoint()
    {
        Inventory.instance.score += addPoint;
    }
}
