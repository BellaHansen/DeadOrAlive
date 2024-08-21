using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] weaponStats item;


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var weaponControl = other.GetComponent<WeaponController>();
            if (weaponControl != null)
            {
                weaponControl.equipWeapon(item);
            }

            Destroy(gameObject);

        }
    }

}
