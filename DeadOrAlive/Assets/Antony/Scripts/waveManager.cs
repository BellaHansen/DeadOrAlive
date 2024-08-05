using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveManager : MonoBehaviour
{
    //Instance for the wave manager 
    public static waveManager instance;

    //Variable for the zombie spawner
    public ZombieSpawn[] spawners;

    //Variable for the time between waves
    [SerializeField] float timeBetweenWaves;
    [SerializeField] List<GameObject> zombieModel;
    [SerializeField] float startZombieCount;

    //Variable for current wave
    public int currentWave;
    private int zombiesKilled = 0;
    private int zombiesToSpawn = 0;
    private bool wavesStopped = false;
    private Coroutine waveCoroutine;


    // Start is called before the first frame update
    //Change start for swake
    void Awake()
    {
        //Instantiate
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        waveCoroutine = StartCoroutine(startWave());
    }

    //Create IEnumerator to start wave
    public IEnumerator startWave()
    {
        //Increase the current wave
        currentWave++;

        if(currentWave <= spawners.Length)
        {
            yield return new WaitForSeconds(timeBetweenWaves);


            spawners[currentWave - 1].startSpawning();
            zombiesToSpawn = Mathf.RoundToInt(startZombieCount * currentWave); // Example logic
        }
        else
        {
            // If no more spawners are available, stop waves
            StopWaves();
            yield break;
        }
    }
    public void ZombieKilled()
    {
        zombiesKilled++;
        if (zombiesKilled >= zombiesToSpawn)
        {
            zombiesKilled = 0;
            if (!wavesStopped && waveCoroutine == null)
            {
                waveCoroutine = StartCoroutine(startWave());
            }
        }
    }

    public void StopWaves()
    {
        wavesStopped = true;
        if (waveCoroutine != null)
        {
            StopCoroutine(waveCoroutine);
            waveCoroutine = null;
        }
        Debug.Log("You Win");
    }
}
