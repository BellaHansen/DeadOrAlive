using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Item", menuName = "Inventory/Inventory Item")]

public abstract class inventoryItem : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemIcon;
    [SerializeField] private int itemNum;
    [SerializeField] public int Damage;

    [SerializeField] private GameObject itemModel;
    public abstract void Attack(Vector3 position, Quaternion rotation);
}
