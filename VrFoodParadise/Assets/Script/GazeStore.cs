using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeStore : GazeAt
{
    [SerializeField] private BoxCollider storeCollider;
    [SerializeField] private GameObject menuPanel;

    void Start()
    {
        //storeCollider = this.GetComponent<BoxCollider>();
        menuPanel.SetActive(false);

        Initialize();
    }

    void Update()
    {
        CloseCollider(storeCollider);

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
