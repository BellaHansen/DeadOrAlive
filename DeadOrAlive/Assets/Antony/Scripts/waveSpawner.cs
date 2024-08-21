using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] thingToSpawn;
    [SerializeField] int spawnTimer;
    [SerializeField] int numToSpawn;
    [SerializeField] Transform[] spawnPos;
    [SerializeField] GameObject spawnEffect;

    int spawnCount;
    bool isSpawning;
    bool startSpawning;
    int numKilled;


    // Update is called once per frame
    void Update()
    {
        if (startSpawning && !isSpawning && spawnCount < numToSpawn)
        {
            StartCoroutine(Spawn());
        }
    }

    public void StartWave()
    {
        startSpawning = true;
        
      

    }

    IEnumerator Spawn()
    {
        isSpawning = true;
        GameObject enemyPrefab = thingToSpawn[Random.Range(0, thingToSpawn.Length)];
        int arrayPos = Random.Range(0, spawnPos.Length);

        GameObject effect = Instantiate(spawnEffect, spawnPos[arrayPos].position, spawnPos[arrayPos].rotation);
        Destroy(effect, 2f);

        yield return new WaitForSeconds(2f);

        GameObject objectSpawned = Instantiate(enemyPrefab, spawnPos[arrayPos].position, spawnPos[arrayPos].rotation);

        if (objectSpawned.GetComponent<zombieAI>())
        {
            objectSpawned.GetComponent<zombieAI>().whereISpawned1 = this;
        }
        else if (objectSpawned.GetComponent<newSpitterAI>())
        {
            objectSpawned.GetComponent<newSpitterAI>().whereISpawned1 = this;
        }

        spawnCount++;
       
        yield return new WaitForSeconds(spawnTimer);
        isSpawning=false;
        
    }

    public void updateEnemyNumber()
    {
        numKilled++;
        if(numKilled >= numToSpawn)
        {
            startSpawning = false;

            StartCoroutine(waveManager.instance.StartWave());

        }
    }
}
