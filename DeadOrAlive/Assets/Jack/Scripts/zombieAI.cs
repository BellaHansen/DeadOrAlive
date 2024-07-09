using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class zombieAI : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Renderer model;
    [SerializeField] Color damageColor;

    [SerializeField] int HP;
    [SerializeField] int faceTargetSpeed;
    [SerializeField] GameObject target;

    Vector3 targetDir;

    bool isBeingDamaged;

    void Start()
    {
        isBeingDamaged = false;
        model.material.EnableKeyword("_EMISSION");
    }

    void Update()
    {
        targetDir = target.transform.position - transform.position;
        agent.SetDestination(target.transform.position);

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            faceTarget();
        }

        if (!isBeingDamaged)
        {
            StartCoroutine(flashDamage());
        }
    }
    void faceTarget()
    {
        Quaternion rot = Quaternion.LookRotation(targetDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * faceTargetSpeed);
    }
    IEnumerator flashDamage()
    {
        isBeingDamaged = true;
        model.material.SetColor("_EmissionColor", damageColor);
        yield return new WaitForSeconds(1f);
        model.material.SetColor("_EmissionColor)", Color.black);
        isBeingDamaged = false;
        yield return new WaitForSeconds(1f);
    }
}
