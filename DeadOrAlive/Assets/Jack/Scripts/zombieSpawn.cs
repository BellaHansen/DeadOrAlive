using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieSpawn : MonoBehaviour
{
    [SerializeField] GameObject thingToSpawn;
    [SerializeField] int numToSpawn;
    [SerializeField] int spawnTimer;
    [SerializeField] Transform[] spawnPos;

    int spawnCount;
    bool isSpawning;
    bool startSpawning;
    int numKilled;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (startSpawning && !isSpawning && spawnCount < numToSpawn)
        {
            StartCoroutine(spawn());
        }
    }

    public void startWave()
    {
        startSpawning = true;
        gameManager.instance.updateGameGoal(numToSpawn);
    }

    IEnumerator spawn()
    {
        isSpawning = true;
        int arrayPos = Random.Range(0, spawnPos.Length);
        GameObject objectSpawned = Instantiate(thingToSpawn, spawnPos[arrayPos].position, spawnPos[arrayPos].rotation);
        
        if (objectSpawned.GetComponent<zombieAI>())
        {
            objectSpawned.GetComponent<zombieAI>().whereISpawned = this;
        }

        spawnCount++;
        yield return new WaitForSeconds(spawnTimer);
    }

    public void updateEnemyNumber()
    {
        numKilled++;

        if (numKilled >= numToSpawn)
        {
            startSpawning = false;
        }
    }
}
