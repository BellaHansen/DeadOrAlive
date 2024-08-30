using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New weapon Item", menuName = "Inventory/Weapon  Item")]
public class weaponStats : ScriptableObject
{
    [SerializeField] public GameObject itemModel;
    [SerializeField] public int itemDamage;
    [SerializeField] public int itemDistance;
    [SerializeField] public float shootRate;
    [SerializeField] public int ammoCur;
    [SerializeField] public int ammoMax;
    [SerializeField] public AudioClip[] shootSound;
    [SerializeField] public float shootVol;
}
