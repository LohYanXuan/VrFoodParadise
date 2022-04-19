using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodRecipe : MonoBehaviour
{
    public List<string> ingredientInIt = new List<string>();

    private void Awake()
    {
        ingredientInIt.Clear();
    }
}
