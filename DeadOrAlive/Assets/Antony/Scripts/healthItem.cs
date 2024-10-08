using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Health Item", menuName = "Inventory/Health Item")]

public class healthItem : ScriptableObject
{
    //Variable for health value
    [SerializeField] int healthVal;
    
    //method to return health item value
    public int GetHealthValue()
    {
        return healthVal;
    }

}
