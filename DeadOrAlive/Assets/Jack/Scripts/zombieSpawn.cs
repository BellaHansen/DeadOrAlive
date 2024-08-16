using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieSpawn : MonoBehaviour
{
    [SerializeField] GameObject thingToSpawn;
    [SerializeField] int spawnTimer;
    [SerializeField] Transform[] spawnPos;

    private int numToSpawn;
    private int spawnCount;
    private bool isSpawning;
    private bool startSpawning;
    private int numKilled;

    private waveManager waveManager;

    private void Start()
    {
        waveManager = waveManager.instance;
    }

    private void Update()
    {
        if (startSpawning && !isSpawning && spawnCount < numToSpawn)
        {
            StartCoroutine(Spawn());
        }
    }

    public void StartWave(int zombiesToSpawn)
    {
        numToSpawn = zombiesToSpawn; // Set number of zombies to spawn
        startSpawning = true;
        gameManager.instance.updateGameGoal(numToSpawn); // Notify the game manager
    }

    private IEnumerator Spawn()
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

        isSpawning = false; // Set isSpawning to false after spawning
    }

    public void UpdateEnemyNumber()
    {
        numKilled++;

        if (numKilled >= numToSpawn)
        {
            startSpawning = false;
            waveManager.ZombieKilled();
            gameManager.instance.updateGameGoal(-numToSpawn);
          
        }
    }
}
