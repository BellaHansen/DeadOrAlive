using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    public List<inventoryItem> items = new List<inventoryItem>();
    public int maxItems = 4;
    private int currentItemIndex = 0;


    public bool AddItem(inventoryItem item)
    {
        if (items.Count >= maxItems)
        {
            Debug.Log("Inventory is full");
            return false;
        }
        items.Add(item);
        return true;
    }

    public bool RemoveItem(inventoryItem item)
    {
        return items.Remove(item);
    }
    public inventoryItem GetCurrentItem()
    {
        if (items.Count == 0) return null;
        else
        return items[currentItemIndex];
    }

    public void NextItem()
    {
        //used to go switch to next item
        if (items.Count == 0) return;
        currentItemIndex = (currentItemIndex + 1) % items.Count;
    }

    public void PreviousItem()
    {
        //used to go switch to previous item
        if (items.Count == 0) return;
        currentItemIndex = (currentItemIndex - 1 + items.Count) % items.Count;
    }
}
