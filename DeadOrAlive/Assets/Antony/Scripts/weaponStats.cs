using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class weaponStats : ScriptableObject
{
    //Variable for gun model
    public GameObject weaponModel;

    //Variable for shooting damage
    public int shootdamage;

    //Variable for shooting distance
    public int shootDist;

    //Variable for shooting rate
    public float shootRate;

    //Variable for current ammo
    public int ammoCur;

    //Variable for maximum ammo 
    public int ammoMax;

}
