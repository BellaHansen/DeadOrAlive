using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponPickup : MonoBehaviour
{
    //Variable for gun stats gun
    [SerializeField] weaponStats gun;

    //Create on trigger enter 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Set gun ammo current equals to gun ammo maximum
            gun.ammoCur = gun.ammoMax;

            gameManager.instance.playerScript.GetGunStats(gun);

            Destroy(gameObject);
        }

    }

}
