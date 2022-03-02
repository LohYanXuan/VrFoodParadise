using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            items.Clear();
        }
    }

    public void InsertItems(GameObject objects)
    {
        items.Add(objects);
    }

    public void ClearItems()
    {
        items.Clear();
    }
}
