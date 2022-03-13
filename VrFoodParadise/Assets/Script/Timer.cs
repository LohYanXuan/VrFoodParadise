using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float totalSeconds = 0;
    float elapsedSeconds = 0;
    bool running = false;
    //public int shuffle;
    [SerializeField] int minOrder = 3, maxOrder = 7; 
    [SerializeField] int minQ = 1,  maxQ = 5;
  
    //List is for customer to store & output their orders
    // Orders are randomly churned. Randomness affects 
    // quantity and ingredient type. 
    // Burger ingredients: bun, sauce, tomatoes, lettuce, cheese, egg, meat
    // Customer sld choose 5 ing minimum, 
    List<string> ingredients = new List<string> { "Bun", "Sauce", "Tomato", "Meat", "Lettuce", "Cheese", "Egg" };
    List<int> iQuantity = new List<int>();

    void start()
    {
        running = true;
    }
        
    void Update()
    {
        if (running)
        {
            elapsedSeconds += Time.deltaTime;
            if (elapsedSeconds >= totalSeconds)
            {
                running = false;
                Run();
            }
        }
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
        for(int i = 0; i < count; i++)
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
    }

}
