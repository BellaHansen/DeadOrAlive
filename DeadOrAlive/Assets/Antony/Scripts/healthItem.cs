using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthItem : MonoBehaviour
{
    //Variable for health value
    [SerializeField] int healthVal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            //Get the IDamage component from the player
            IDamage player = other.GetComponent<IDamage>();
            if (player != null)
            {
                //Call takeDamage on the  player to heal
                player.takeDamage(healthVal - healthVal);
            }

            //Destroy the health item after it has been collected
            Destroy(gameObject);
        }
    }

}
