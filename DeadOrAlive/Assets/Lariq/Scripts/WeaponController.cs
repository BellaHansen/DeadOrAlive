using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    //simple class to manage weapon equipping (referenced by playerController and gameManager)
    [SerializeField] private Transform weaponMountPoint;
    private weaponStats currentWeapon;
    private int currentAmmo;
    private int maxAmmo;

    public void equipWeapon(weaponStats weapon)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.itemModel);
        }

        GameObject weaponItem = Instantiate(weapon.itemModel, weaponMountPoint.position, weaponMountPoint.rotation, weaponMountPoint);

        

        maxAmmo = currentWeapon.ammoMax;
        currentAmmo = currentWeapon.ammoCur;
    }
        public int getCurrentAmmo()
    {
        return currentAmmo;
    }

    public int getMaxAmmo()
    {
        return maxAmmo;
    }
    public void SetCurrentAmmo(int ammo)
    {
        // sets range between 0 and maxAmmo
        currentAmmo = Mathf.Clamp(ammo, 0, maxAmmo);
    }

    public void SetMaxAmmo(int ammo)
    {
        maxAmmo = ammo;
        currentAmmo = Mathf.Clamp(currentAmmo, 0, maxAmmo);
    }
}

