using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Health Item", menuName = "Inventory/Health Item")]

public class healthItem : inventoryItem
{
    //Variable for health value
    [SerializeField] int healthVal;

    public override void Use(GameObject player)
    {
        PlayerController controller = player.GetComponent<PlayerController>();
        if (controller != null)
        {
        }
    }

    //method to return health item value
    public int GetHealthValue()
    {
        return healthVal;
    }

}
