using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryItem
{
    string ItemName { get; }
    Sprite ItemIcon { get; }
    int ItemNum { get; }
    GameObject ItemModel { get; }
    void OnPickup();
    void OnDrop();

}

