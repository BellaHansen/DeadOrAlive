using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    //simple class to manage weapon equipping (referenced by playerController and gameManager)
    [SerializeField] private Transform weaponMountPoint;
    private Weapons currentWeapon;
    private int currentAmmo;
    private int maxAmmo;

    public void equipWeapon(Weapons weapon)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.weaponModel);
        }

        currentWeapon = Instantiate(weapon.weaponModel, weaponMountPoint).GetComponent<Weapons>();
    }
    public void useCurrentWeapon(Vector3 position, Quaternion rotation)
    {
        if (currentWeapon != null)
        {
            currentWeapon.Attack(position, rotation);
        }
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

