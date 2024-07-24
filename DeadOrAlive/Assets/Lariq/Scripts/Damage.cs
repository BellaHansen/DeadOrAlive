using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public enum DamageType { Bullet, Melee }
    public enum WeaponType { Pistol, AssaultRifle, Knife, Shotgun }

    [SerializeField] private DamageType damageType;
    [SerializeField] private WeaponType weaponType;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private int baseDamageAmount;
    [SerializeField] private int speed;
    [SerializeField] private int destroyTime;
    [SerializeField] private inventoryItem weaponData;
    [SerializeField] private int damage;
    private bool hasDamaged;

    // Start is called before the first frame update
    void Start()
    {
        if (damageType == DamageType.Bullet)
        {
            rb.velocity = transform.forward * speed;
            Destroy(gameObject, destroyTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger) return;

        IDamage dmg = other.GetComponent<IDamage>();

        if (dmg != null && !hasDamaged)
        {
            ApplyDamage(dmg);
            hasDamaged = true;

            if (damageType == DamageType.Bullet)
            {
                Destroy(gameObject);
            }
        }
    }

    private void ApplyDamage(IDamage dmg)
    {
        int calculatedDamage = baseDamageAmount;

        switch (weaponType)
        {
            case WeaponType.Pistol:
                calculatedDamage = CalculatePistolDamage();
                break;
            case WeaponType.AssaultRifle:
                calculatedDamage = CalculateAssaultRifleDamage();
                break;
            case WeaponType.Shotgun:
                calculatedDamage = CalculateShotgunDamage();
                break;
            case WeaponType.Knife:
                calculatedDamage = CalculateMeleeDamage();
                break;
        }

        dmg.TakeDamage(calculatedDamage);
    }

    private int CalculatePistolDamage()
    {
        return weaponData.Damage + 5;
    }

    private int CalculateAssaultRifleDamage()
    {
        return weaponData.Damage + 10;
    }

    private int CalculateShotgunDamage()
    {
        return weaponData.Damage + 50;
    }

    private int CalculateMeleeDamage()
    {
        return weaponData.Damage;
    }
}
