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
    public List<GameObject> foods = new List<GameObject>();
    
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
    }

    public void InsertFoods(GameObject objects)
    {
        foods.Add(objects);
    }

    public void ClearFoods()
    {
        for (i = 0; i < foods.Count; i++)
        {
            foods[i].gameObject.SetActive(true);
        }

        foods.Clear();
    }

    public void InsertIngredients(GameObject objects)
    {
        ingredients.Add(objects);
        Debug.Log(objects.GetComponent<GazeMenu>().tagName);
    }

    public void ClearIngredients()
    {
        for (i = 0; i < ingredients.Count; i++)
        {
            ingredients[i].gameObject.SetActive(true);
        }

        ingredients.Clear();
    }

    //Remove specific object at specific index
    public void ListRemoveAtIndex(List<GameObject> list, int index)
    {
        Destroy(list[index]);
        list.RemoveAt(index);
    }
}
