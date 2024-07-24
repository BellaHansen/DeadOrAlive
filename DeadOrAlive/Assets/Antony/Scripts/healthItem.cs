using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthItem : MonoBehaviour
{
    //Variable for health value
    [SerializeField] int healthVal;
  
    private void OnTriggerEnter(Collider other)
    {
        //Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            //Call the heal method from player controller
            //other.GetComponent<PlayerController>().HealPlayer(healthVal);

            //Destroy the health item after it has been collected
            Destroy(gameObject);
        }
    }

}
