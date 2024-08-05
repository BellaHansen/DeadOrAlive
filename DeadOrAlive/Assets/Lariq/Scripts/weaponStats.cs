using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New weapon Item", menuName = "Inventory/Weapon  Item")]
public class weaponStats : ScriptableObject
{
    public GameObject itemModel;
    public Sprite itemIcon;
    public int itemDamage;
    public int itemRange;
    public float shootRate;
    public int ammoCur;
    public int ammoMax;

    public ParticleSystem itemEffect;
    public AudioClip itemSound;
    public float shootVol;

    public GameObject Item => itemModel;


}
public class InventoryEvents : EventArgs
{
    public InventoryEvents(weaponStats item)
    {
        Items = item;
    }
    weaponStats Items { get; }
}

