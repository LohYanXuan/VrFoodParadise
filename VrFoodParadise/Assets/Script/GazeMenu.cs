using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GazeMenu : GazeAt
{
    public enum ingredientTag { Bun, Sauce, Tomato, Meat, Lettuce, Cheese, Egg };
    public ingredientTag tagName;

    [SerializeField] private Text ingredentCountText;

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        StoreInInventory();

        if (ingredentCountText != null)
        {
            ingredentCountText.text = ingredentCount.ToString();
        }
    }
}
