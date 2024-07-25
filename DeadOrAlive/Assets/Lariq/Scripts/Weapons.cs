using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapons : inventoryItem
{
    public enum WeaponType
    {
        Pistol,
        AssaultRifle,
        Knife,
        Shotgun
    }
    //can be pistol,knife, AssaultRifle
    [SerializeField] private WeaponType weaponType;
    //put actual weapon model
    [SerializeField] public GameObject weaponModel;
    //how much damage can deal
    [SerializeField] private int damage;
    //how far it can go
    [SerializeField] private int range;
    //how fast weapon attack or shoot
    [SerializeField] private AudioClip attackSound;

    public abstract void Attack(Vector3 position, Quaternion rotation);

    public override void Use(GameObject player)
    {
        WeaponController weaponController = player.GetComponent<WeaponController>();
        if (weaponController != null)
        {
            weaponController.equipWeapon(this);
        }
    }
}
