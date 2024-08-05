using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInteraction : MonoBehaviour
{
    public Inventory inventory;

    // Update is called once per frame
    void Update()
    {
        PickUpItem();
        SwitchItems();
    }

    void PickUpItem()
    {
        if (Input.GetButtonDown("PickUp"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Item"))
                {
                    // Checks if the item is able to be picked up
                    itemPickup itemPickups = hit.transform.GetComponent<itemPickup>();
                    if (itemPickups != null)
                    {
                        weaponStats item = itemPickups.weapon;
                        if (inventory.AddItem(item))
                        {
                            Destroy(hit.transform.gameObject);
                        }
                    }
                }
            }
        }
    }

    void SwitchItems()
    {
        // Goes to the next item
        if (Input.GetButtonDown("NextItem"))
        {
            inventory.NextItem();
        }
        // Goes to the previous item
        if (Input.GetButtonDown("PreviousItem"))
        {
            inventory.PreviousItem();
        }
    }
}
