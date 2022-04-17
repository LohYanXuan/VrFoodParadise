using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeMenu : GazeAt
{
    public enum ingredientTag { Bun, Sauce, Tomato, Meat, Lettuce, Cheese, Egg };
    public ingredientTag tagName;

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        StoreInInventory();
    }
}
