using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeStore : GazeAt
{
    Inventory inventory;
    [SerializeField] private BoxCollider storeCollider;
    [SerializeField] private GameObject menuPanel;

    void Start()
    {
        inventory = Inventory.instance;

        //storeCollider = this.GetComponent<BoxCollider>();
        menuPanel.SetActive(false);

        Initialize();
    }

    void Update()
    {
        //if (inventory.foods == null)
        //{
            CloseCollider(storeCollider);
        //}

        if (storeCollider.enabled)
        {
            menuPanel.SetActive(false);
        }
        else
        {
            GVROff();
            menuPanel.SetActive(true);
        }
    }
}
