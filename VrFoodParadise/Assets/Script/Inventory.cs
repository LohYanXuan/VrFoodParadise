using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();

    int i = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ClearItems();
        }
    }

    public void InsertItems(GameObject objects)
    {
        items.Add(objects);
    }

    public void ClearItems()
    {
        for (i = 0; i < items.Count; i++)
        {
            items[i].gameObject.SetActive(true);
        }

        items.Clear();
    }
}
