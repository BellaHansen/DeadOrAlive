using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class damage : MonoBehaviour
{
    [SerializeField] enum damageType { bullet, stationary }
    [SerializeField] damageType type;

    [SerializeField] Rigidbody rb;
    [SerializeField] int damageAmount;
    [SerializeField] int speed;
    [SerializeField] int destroyTime;
    [SerializeField] GameObject acidPool;
    [SerializeField] GameObject spitterPrefab;

    bool hasDamaged;

    // Start is called before the first frame update
    void Start()
    {
        if (type == damageType.bullet)
        {
            Vector3 direction = (gameManager.instance.player.transform.position - transform.position).normalized;
            rb.velocity = direction * speed;
            Destroy(gameObject, destroyTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }
            IDamage dmg = other.GetComponent<IDamage>();

            if (dmg != null && !hasDamaged)
            {
                dmg.TakeDamage(damageAmount);
            hasDamaged = true;
            }

        if (other.tag == "Floor")
        {
            Instantiate(acidPool, gameObject.transform.position, Quaternion.Euler(0, 0, 0));
            Destroy(gameObject);
        }
    }
}
