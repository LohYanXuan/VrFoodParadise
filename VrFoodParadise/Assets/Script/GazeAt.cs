using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GazeAt : MonoBehaviour
{
    [Header("GameManager - Inventory")]
    public GameObject gameInventory;
    private Inventory inventory;

    [Header("Object Settings")]
    private Renderer myRenderer;
    Material oriMat;
    public Material gazeMat;

   


    private float startTime;
    private float timer;
    bool isResetTimer;

    public bool isGaze;

    void Start()
    {

        inventory = gameInventory.GetComponent<Inventory>();
        myRenderer = GetComponent<Renderer>();
        oriMat = myRenderer.material;

        

        startTime = 0;
        timer = 0;

        isResetTimer = false;
        isGaze = false;
    }

    void Update()
    {
        OutlineWhenGaze();
        StoreInInventory();
    }

    public void SetGazeAt(bool gazeAt)
    {
        isGaze = gazeAt;
    }

    private void OutlineWhenGaze()
    {
        if (isGaze)
        {
            myRenderer.material = gazeMat;
        }
        else
        {
            myRenderer.material = oriMat;
        }
    }

    private void StoreInInventory()
    {
        if (isGaze)
        {
            if (!isResetTimer)
            {
                startTime = Time.time;
                timer = Time.time;
                isResetTimer = true;
            }
            else
            {
                timer += Time.deltaTime;

                if (timer - startTime >= 2)
                {
                    inventory.InsertItems(this.gameObject);
                    isGaze = false;
                    gameObject.SetActive(false);
                }
            }
        }
        else
        {
            startTime = 0;
            timer = 0;
            isResetTimer = false;
        }
    }
}
