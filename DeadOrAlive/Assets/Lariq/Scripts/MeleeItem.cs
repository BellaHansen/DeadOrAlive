using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Melee Item", menuName = "Inventory/Melee Item")]
public class MeleeItem : Weapons
{
    [SerializeField] private GameObject meleeModel;
    [SerializeField] private int meleeDamage;
    [SerializeField] private int meleeDistance;
    [SerializeField] private float attackRate;
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private AudioClip meleeSound;
    [SerializeField] private float lastAttack;
    public override void Attack(Vector3 position, Quaternion rotation)
    {
        if (Time.time - lastAttack < attackRate)
            return;

        // Play mele
        AudioSource.PlayClipAtPoint(meleeSound, position);

        // checks to see if two object colliders are hitting
        Collider[] hitColliders = Physics.OverlapSphere(position, meleeDistance);
        foreach (var hitCollider in hitColliders)
        {
            IDamage dmg = hitCollider.GetComponent<IDamage>();
            if (dmg != null)
            {
                dmg.TakeDamage(meleeDamage);
            }
        }

        //shows hiteffect if objects touch
        if (hitEffect != null)
        {
            Instantiate(hitEffect, position,rotation);
        }

        lastAttack = Time.time;
    }
}
