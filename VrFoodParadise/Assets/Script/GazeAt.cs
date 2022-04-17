using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class GazeAt : MonoBehaviour
{
    Inventory inventory;
    public enum listType { food, ingredient, none };
    public listType inventoryType;

    [Header("Object Settings")]
    private Renderer myRenderer;
    Material oriMat;
    public Material gazeMat;

    private float startTime;
    private float timer;
    bool isResetTimer;

    protected bool isGaze;

    public void Initialize()
    {
        inventory = Inventory.instance;

        if (GetComponent<Renderer>() != null)
        {
            myRenderer = GetComponent<Renderer>();
            oriMat = myRenderer.material;
        }

        startTime = 0;
        timer = 0;

        isResetTimer = false;
        isGaze = false;
    }

    //void Update()
    //{
    //    OutlineWhenGaze();
    //    StoreInInventory();
    //}

    public void SetGazeAt(bool gazeAt)
    {
        isGaze = gazeAt;
    }

    private void OutlineWhenGaze()
    {
        if (gazeMat != null)
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
    }

    public void StoreInInventory()
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
                    if (inventoryType == listType.food)
                    {
                        inventory.InsertFoods(this.gameObject);
                    }
                    if (inventoryType == listType.ingredient)
                    {
                        inventory.InsertIngredients(this.gameObject);
                    }

                    //Reset timer for keep gazing at same thing
                    //isGaze = false;
                    startTime = 0;
                    timer = 0;
                    isResetTimer = false;

                    //gameObject.SetActive(false);
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

    public void CloseCollider(Collider collider)
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
                    isGaze = false;
                    startTime = 0;
                    timer = 0;
                    isResetTimer = false;

                    collider.enabled = false;
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
