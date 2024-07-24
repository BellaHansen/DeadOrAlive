using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Health Item", menuName = "Inventory/Health Item")]

public class healthItem : inventoryItem
{
    //Variable for health value
    [SerializeField] int healthVal;

    public override void Attack(Vector3 position, Quaternion rotation)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            //Call the heal method from player controller
            //other.GetComponent<PlayerController>().HealPlayer(healthVal);

            //Destroy the health item after it has been collected
            Destroy(this);
        }
    }

}
