using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //Singleton
    #region Singleton

    public static Inventory instance;

    private void Awake()
    {
        {
            instance = this;
        }
    }

    #endregion

    [Header("Food list")]
    //public List<GameObject> foods = new List<GameObject>();
    public GameObject foods;
    
    [Header("Ingredient list")]
    public List<GameObject> ingredients = new List<GameObject>();


    int i = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ClearFoods();
        }

        //Debug.Log(foods[0].GetComponent<FoodRecipe>().ingredientInIt[0]);
        //Debug.Log(foods[0].GetComponent<FoodRecipe>().ingredientInIt[1]);
        //Debug.Log(foods[0].GetComponent<FoodRecipe>().ingredientInIt[2]);
    }

    public void InsertFoods(GameObject objects)
    {
        //foods.Add(objects);
        foods = objects;
    }

    public void ClearFoods()
    {
        //foods.Clear();
        Destroy(foods);
        foods = null;
    }

    public void InsertIngredients(GameObject objects)
    {
        ingredients.Add(objects);
    }

    public void ClearIngredients()
    {
        //for (i = 0; i < ingredients.Count; i++)
        //{
        //    ingredients[i].gameObject.SetActive(true);
        //}

        ingredients.Clear();
    }

    //Remove specific object at specific index
    public void ListRemoveAtIndex(List<GameObject> list, int index)
    {
        Destroy(list[index]);
        list.RemoveAt(index);
    }

    public bool CheckCustomerOrder(List<string> cusOrder)
    {
        int i, j, k;

        //for (i = 0; i < foods.Count; i++)
        //{
            FoodRecipe foodRecipe = foods.GetComponent<FoodRecipe>();
            List<string> tempList = new List<string>(foodRecipe.ingredientInIt);

        //If the amount of ingredients in food is same with customer's order
        if (foodRecipe.ingredientInIt.Count == cusOrder.Count)
        {
            //Go through cutomer order one by one
            for (j = 0; j < cusOrder.Count; j++)
            {
                for (k = 0; k < tempList.Count; k++)
                {
                    if (cusOrder[j] == tempList[k])
                    {
                        //Remove when same ingredient is found
                        tempList.RemoveAt(k);

                        //After all same ingredients are removed, means all ingredients are correct
                        if (tempList.Count == 0)
                        {
                            //ListRemoveAtIndex(foods, i);
                            ClearFoods();
                            return true;
                        }

                        //Quit current loop and go to next element in customer order
                        break;
                    }
                    if (k++ >= tempList.Count)
                    {
                        //Go through all ingredients in food but still doesn't match customer order
                        //End checking
                        j = cusOrder.Count;
                    }
                }
            }
        }
        else
            Debug.Log("AAAAAAAAAAAAAAAAAAAAA");
        //}
        return false;
    }
}
