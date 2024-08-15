using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveManager : MonoBehaviour
{
    // Static instance for singleton pattern
    public static waveManager instance;

    // Array of zombie spawners
    public zombieSpawn[] spawners;

    // Time between waves
    [SerializeField] float timeBetweenWaves;
    [SerializeField] List<GameObject> zombieModel;
    [SerializeField] float startZombieCount;

    // Variables for wave management
    public int currentWave;
    private int zombiesKilled = 0;
    private int zombiesToSpawn = 0;
    private bool wavesStopped = false;
    private Coroutine waveCoroutine;

    private void Awake()
    {
        // Singleton pattern implementation
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Start the first wave
        waveCoroutine = StartCoroutine(StartWave());
    }

    // Coroutine to start a new wave
    public IEnumerator StartWave()
    {
        // Increment the wave counter
        currentWave++;

        if (currentWave <= spawners.Length)
        {
            // Wait before starting the new wave
            yield return new WaitForSeconds(timeBetweenWaves);

            // Calculate the number of zombies to spawn
            zombiesToSpawn = Mathf.RoundToInt(startZombieCount * currentWave);
            zombiesKilled = 0; // Reset the number of zombies killed for the new wave

            // Start spawning zombies using the appropriate spawner
            spawners[currentWave - 1].StartWave(zombiesToSpawn);
        }
        else
        {
            // All waves completed, stop spawning
            StopWaves();
        }
    }

    // Method called when a zombie is killed
    public void ZombieKilled()
    {
        zombiesKilled++;

        // Check if all zombies in the current wave are killed
        if (zombiesKilled >= zombiesToSpawn)
        {
            zombiesKilled = 0; // Reset for the next wave

            if (!wavesStopped)
            {
                // Stop the current wave coroutine and start the next wave
                if (waveCoroutine != null)
                {
                    StopCoroutine(waveCoroutine);
                }
                waveCoroutine = StartCoroutine(StartWave());
            }
        }
    }

    // Check if all waves have been completed
    public bool AllWavesCompleted()
    {
        return currentWave >= spawners.Length;
    }

    // Stop all waves and declare victory
    public void StopWaves()
    {
        wavesStopped = true;
        if (waveCoroutine != null)
        {
            StopCoroutine(waveCoroutine);
        }
        gameManager.instance.updateGameGoal(0); 
    }
}