using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class acidPool : MonoBehaviour
{
    [SerializeField] int damageAmount;
    [SerializeField] float timeBetweenDamage;
    

    IDamage dmg;
    bool playerInPool;
    bool damage;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DamageAfterTime());
    }
    private void Update()
    {
        if (playerInPool && !damage)
        {
            new WaitForSeconds(timeBetweenDamage);
            StartCoroutine(DamageAfterTime());
        }    
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }
        playerInPool = true;
        dmg = other.GetComponent<IDamage>();
    }

    void OnTriggerExit()
    {
        playerInPool = false;
    }
    
    IEnumerator DamageAfterTime()
    {
        damage = true;
        if (dmg != null)
        {
            dmg.TakeDamage(damageAmount);
            Destroy(gameObject);
        }
        yield return new WaitForSeconds(timeBetweenDamage);
        damage = false;
    }
}
