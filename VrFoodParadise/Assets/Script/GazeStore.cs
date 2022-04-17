using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeStore : GazeAt
{
    private Collider storeCollider;
    [SerializeField] private GameObject menuPanel;

    void Start()
    {
        storeCollider = this.GetComponent<Collider>();
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
            menuPanel.SetActive(true);
        }
    }
}
