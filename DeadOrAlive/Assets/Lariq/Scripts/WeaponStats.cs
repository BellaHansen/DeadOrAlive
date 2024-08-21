using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponStats : ScriptableObject
{
    public GameObject itemModel;
    public int itemDamage;
    public int itemRange;
    public float shootRate;
    public int ammoCur;
    public int ammoMax;

    public ParticleSystem hitEffect;
    public AudioClip shootSound;
    public float shootVol;
}
