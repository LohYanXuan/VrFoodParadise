using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpawnDish : MonoBehaviour
{
    Inventory inventory;

    [Header("GameManager - SpawnIngridient")]
    public GameObject ingridientType;
    public List<UnityEngine.Object> clones = new List<UnityEngine.Object>();
    public GameObject holdPosition;
    

    [Header("Object Settings")]
    private Renderer myRenderer;
    Material oriMat;
    public Material gazeMat;

    public float timer = 2.0f;
    public float timerTimer = 0;
   
    bool startTimer = false;

    public bool isGaze;
    

    void Start()
    {
        inventory = Inventory.instance;

        myRenderer = GetComponent<Renderer>();
        oriMat = myRenderer.material;
        SetGazeAt(false);

    }

 

    void Update()
    {
        if (startTimer == true)
        {
            timerTimer += Time.deltaTime;
            

            if (timerTimer >= timer)
            {
                SpawnIngridients();
                OutlineWhenGaze();

            }



        }
       
        
       
        
    }

    public void SetGazeAt(bool gazedAt)
    {
      
        if (gazedAt == true)
        {
            startTimer = true;
            myRenderer.material = gazeMat;
        }
        else
        {
            myRenderer.material = oriMat;
            startTimer = false;
            timerTimer = 0;
        }
        return;
    }


    public void SpawnIngridients()
    {


        Vector3 position = holdPosition.transform.position;
        clones.Add(Instantiate(ingridientType, position, Quaternion.identity));
        SetGazeAt(false);

        /* "SetGazeAt(false);" wont stop spawning cuz u set false here, then "event trigger" still running n set back to true
         * the order of execution is event trigger then update, thus ur "false" hvnt reset in update but "event trigger" ady chg it back to true
         * then when update comes, it continues true*/


        //Below is script that handles all list, u hv dif button but all nid save in 1 same list for checking n ur list is each button 1 list
        //inventory.InsertIngredients(Instantiate(ingridientType, position, Quaternion.identity));
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

  
 }





