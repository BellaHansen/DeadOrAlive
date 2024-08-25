using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveManager : MonoBehaviour
{
    // Static instance for singleton pattern
    public static waveManager instance;

    // Array of zombie spawners
    public waveSpawner[] spawners;

    // Time between waves
    [SerializeField] int timeBetweenWaves;

    public int currentWave;



    private void Awake()
    {
        // Singleton pattern implementation
        instance = this;

        StartCoroutine(StartWave());
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

            // Start spawning zombies using the appropriate spawner
            spawners[currentWave - 1].StartWave();
        }
        
    }

    
}