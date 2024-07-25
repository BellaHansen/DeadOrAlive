using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class inventoryItem : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemIcon;
    [SerializeField] private int itemNum;
    [SerializeField] public int Damage;

    [SerializeField] private GameObject itemModel;
    public abstract void Use(GameObject player);
}
