using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Item", menuName = "Inventory/Inventory Item")]

public class inventoryItem : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemIcon;
    [SerializeField] private GameObject itemModel;
    [SerializeField] private AudioClip itemSound;
    [SerializeField] private int itemNum;
}
