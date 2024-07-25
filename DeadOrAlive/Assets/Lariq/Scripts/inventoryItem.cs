using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class inventoryItem : ScriptableObject
{
    [SerializeField] public string itemName;
    [SerializeField] public Sprite itemIcon;
    [SerializeField] public int itemNum;
    [SerializeField] public int Damage;

    [SerializeField] private GameObject itemModel;
    public abstract void Use(GameObject player);
}
