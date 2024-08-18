using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class zombieAI : MonoBehaviour, IDamage
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator anim;
    [SerializeField] Renderer model;
    [SerializeField] Color damageColor;
    [SerializeField] LayerMask ignoreMask;
    [SerializeField] Transform attackPos;
    [SerializeField] Transform headPos;

    [SerializeField] int HP;
    [SerializeField] int faceTargetSpeed;
    [SerializeField] int animSpeed;
    [SerializeField] int viewAngle;
    [SerializeField] int roamDist;
    [SerializeField] int roamTimer;
    [SerializeField] int attackDamage;
    [SerializeField] float attackRange = 2.0f;

    [SerializeField] int attackRate;

    Color colorOrig;

    bool isAttacking;
    bool playerInRange;
    bool isRoaming;

    float angleToPlayer;
    float stoppingDistOrig;
    float speedOrig;

    Vector3 playerDir;
    Vector3 startingPos;

    public waveSpawner whereISpawned1;

    void Start()
    {
        colorOrig = model.material.color;
        startingPos = transform.position;
        stoppingDistOrig = agent.stoppingDistance;
        speedOrig = agent.speed;
      
        gameManager.instance.updateGameGoal(1);
    }

    void Update()
    {
        Debug.DrawRay(attackPos.transform.position, attackPos.transform.forward * 3, Color.green);
        float agentSpeed = agent.velocity.normalized.magnitude;
        anim.SetFloat("Speed", Mathf.Lerp(anim.GetFloat("Speed"), agentSpeed, Time.deltaTime * animSpeed));
        if (playerInRange && !canSeePlayer())
        {
            if (!isRoaming && agent.remainingDistance < 0.05f)
            {
                StartCoroutine(roam());
            }
        }
        else if (!playerInRange)
        {
            if (!isRoaming && agent.remainingDistance < 0.05f)
            {
                StartCoroutine(roam());
            }
        }
    }
    IEnumerator roam()
    {
        isRoaming = true;
        yield return new WaitForSeconds(roamTimer);

        agent.stoppingDistance = 0.1f;
        Vector3 randomPos = Random.insideUnitSphere * roamDist;
        randomPos += startingPos;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomPos, out hit, roamDist, 1);
        agent.SetDestination(hit.position);

        isRoaming = false;
    }
    bool canSeePlayer()
    {
        playerDir = gameManager.instance.player.transform.position - headPos.position;
        angleToPlayer = Vector3.Angle(playerDir, transform.forward);

        Debug.DrawRay(headPos.position, playerDir);

        RaycastHit hit;
        if (Physics.Raycast(headPos.position, playerDir, out hit))
        {
            if (hit.collider.CompareTag("Player") && angleToPlayer <= viewAngle)
            {
                agent.SetDestination(gameManager.instance.player.transform.position);

                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    faceTarget();
                }
                if (agent.remainingDistance <= agent.stoppingDistance && !isAttacking && agent.remainingDistance > 0)
                {
                    StartCoroutine(attack());
                }

                agent.stoppingDistance = stoppingDistOrig;
                return true;
            }
        }
        agent.stoppingDistance = 0;
        return false;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            agent.stoppingDistance = 0;
        }
    }
    void faceTarget()
    {
        Quaternion rot = Quaternion.LookRotation(playerDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * faceTargetSpeed);
    }
    IEnumerator attack()
    {
        isAttacking = true;
        agent.speed = 0;
        anim.SetTrigger("Attack");

        yield return new WaitForSeconds(0.5f);

        Collider[] hits = Physics.OverlapSphere(attackPos.position, attackRange, ~ignoreMask);
        foreach (Collider hit in hits)
        {
            IDamage dmg = hit.GetComponent<IDamage>();

            if (dmg != null)
            {
                dmg.TakeDamage(attackDamage);
            }
        }

        yield return new WaitForSeconds(1.5f);
        agent.speed = speedOrig;
        isAttacking = false;
    }
    IEnumerator flashDamage()
    {
        model.material.color = damageColor;
        yield return new WaitForSeconds(0.1f);
        model.material.color = colorOrig;
    }

    public void TakeDamage(int amount)
    {
        HP -= amount;
        agent.SetDestination(gameManager.instance.player.transform.position);
        StartCoroutine(flashDamage());

        if (HP <= 0)
        {
            gameManager.instance.updateGameGoal(-1);
            if (whereISpawned1)
            {
                whereISpawned1.updateEnemyNumber();
            }
            Destroy(gameObject);
        }
    }
}
