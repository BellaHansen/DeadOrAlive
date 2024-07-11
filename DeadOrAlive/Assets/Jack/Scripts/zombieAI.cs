using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class zombieAI : MonoBehaviour, IDamage
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Renderer model;
    [SerializeField] Color damageColor;

    [SerializeField] int HP;
    [SerializeField] int faceTargetSpeed;
    [SerializeField] GameObject target;
    [SerializeField] Animator animator;

    Vector3 targetDir;
    float origSpeed;

    bool isBeingDamaged;

    void Start()
    {
        model.material.EnableKeyword("_EMISSION");
        animator = GetComponent<Animator>();
        isBeingDamaged = false;
        origSpeed = agent.speed;
        target = GameObject.FindWithTag("Player");
        gameManager.instance.updateGameGoal(1);
    }

    void Update()
    {
        targetDir = target.transform.position - transform.position;
        agent.SetDestination(target.transform.position);

        if (agent.remainingDistance <= agent.stoppingDistance && agent.remainingDistance >  0 && animator.GetBool("Attacking") == false)
        {
            StartCoroutine(attack());
        }
        if (animator.GetBool("Attacking") == true)
        {
            faceTarget();
        }    

        if (isBeingDamaged)
        {
            StartCoroutine(flashDamage());
        }   
    }
    IEnumerator attack()
    {
        animator.SetBool("Attacking", true);
        agent.speed = 0;
        yield return new WaitForSeconds(1);
        agent.speed = origSpeed;
        animator.SetBool("Attacking", false);
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
        yield return new WaitForSeconds(1);
        model.material.SetColor("_EmissionColor", Color.black);
        isBeingDamaged = false;
    }

    public void TakeDamage(int Amount)
    {
        HP -= Amount;
    }
}
