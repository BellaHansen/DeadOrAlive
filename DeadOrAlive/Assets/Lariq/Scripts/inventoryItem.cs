using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Item", menuName = "Inventory/Inventory Item")]

public class inventoryItem : ScriptableObject
{
    //creates a scriptable object right click project window create > inventory > inventoory item
    public string itemName;
    public Sprite itemIcon;
    public int itemNum;
}
