using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEditor.Progress;

public class itemPickup : MonoBehaviour
{
    //attach to items that u can pickup
    [SerializeField] public inventoryItem item; // Item to be picked up
    private Inventory inventory;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player has collided with the item
        if (other.CompareTag("Player"))
        {
            // Gets onventory conponent
            Inventory playerInventory = other.GetComponent<Inventory>();
            if (playerInventory != null)
            {
                // Try to add the item to the player inventory
                bool added = playerInventory.AddItem(item);
                if (added)
                {
                    // Destroy the item in the scene
                    Destroy(gameObject);
                }
            }
        }
    }
}
