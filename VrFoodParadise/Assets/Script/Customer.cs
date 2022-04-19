using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

public class Customer : MonoBehaviour
{
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
    [SerializeField] float totalSeconds = 0;
    float elapsedSeconds = 0;
    bool running = false;
    [SerializeField] private Image uiFillImage;
    [SerializeField] private TextMeshProUGUI uiText;
    

    //List is for customer to store & output their orders
    // Orders are randomly churned. Randomness affects 
    // quantity and ingredient type. 
    // Burger ingredients: bun, sauce, tomatoes, lettuce, cheese, egg, meat
    // Customer sld choose 5 ing minimum, 
    List<string> ingredients = new List<string> { "Bun", "Sauce", "Tomato", "Meat", "Lettuce", "Cheese", "Egg" };
    List<int> iQuantity = new List<int>();

    void Awake()
    {
        hasOrder = false;
        customerCanvas.SetActive(false);
        customerOriginalTransform = this.gameObject.transform;
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
                    running = true;
                }
            }
        }
        //if (running)
        //{
        //    elapsedSeconds += Time.deltaTime;
        //    if (elapsedSeconds >= totalSeconds)
        //    {
        //        running = false;
        //        //Run();
        //    }
        //}
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
    void Run()
    {
        if (totalSeconds > 0)
        {
            running = true;

            elapsedSeconds = 0;
            PrintOrder();
        }
    }

    /// <summary>
    /// Get random items from ingredient list
    /// Transfer all randomized items into customer order list
    /// </summary>
    /// <param name="ingredients"></param>
    /// <param name="count"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    List<T> GetRandomIngredients<T>(List<T> ingredients, int count)
    {
        List<T> cusOrder = new List<T>();
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

    private void ResetTimer()
    {
        uiText.text = "00:00";
        uiFillImage.fillAmount = 0f;

        
    }
}
