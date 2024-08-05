using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private int maxItems = 4;
    public List<weaponStats> weapons= new List<weaponStats>();

    private int currentItemIndex = -1;

    public event EventHandler<InventoryEvents> ItemAdded;
    public event EventHandler<InventoryEvents> ItemRemoved;

    public bool AddItem(weaponStats item)
    {
        if (weapons.Count >= maxItems)
        {
            Debug.Log("Inventory is full");
            return false;
        }

        return false;
    }

    public bool RemoveItem(weaponStats item)
    {
        if (weapons.Contains(item))
        {
            // drops inventory item on ground
            weapons.Remove(item);

            return true;
        }

        Debug.LogWarning("{item.ItemName} cant be removed  ");
        return false;
    }

    public weaponStats GetCurrentItem()
    {
        if (weapons.Count == 0)
        {
            Debug.Log("No items in inventory.");
            return null;
        }
        return weapons[currentItemIndex];
    }

    public void NextItem()
    {
        if (weapons.Count == 0) return;
        currentItemIndex = (currentItemIndex + 1) % weapons.Count;
        Debug.Log($"Current item: {weapons[currentItemIndex].name}");
    }

    public void PreviousItem()
    {
        if (weapons.Count == 0) return;
        currentItemIndex = (currentItemIndex - 1 + weapons.Count) % weapons.Count;
        Debug.Log($"Current item: {weapons[currentItemIndex].name}");
    }
  
}
