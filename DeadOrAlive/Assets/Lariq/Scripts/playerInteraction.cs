using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInteraction : MonoBehaviour
{
    public Inventory inventory;

    // Update is called once per frame
    void Update()
    {
        pickUpItem();
    }
    void pickUpItem()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 3f))
            {
                if (hit.transform.CompareTag("Item"))
                {
                    inventoryItem item = hit.transform.GetComponent<itemPickup>().item;
                    if (inventory.AddItem(item))
                    {
                        Destroy(hit.transform.gameObject);
                    }
                }
            }

        }
    }
    void switchItems()
    {
        //goes to next item
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            inventory.NextItem();
        }
        //goes to previs item
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            inventory.PreviousItem();
        }
    }
}
