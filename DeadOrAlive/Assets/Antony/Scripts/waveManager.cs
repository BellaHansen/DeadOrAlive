using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveManager : MonoBehaviour
{
    //Instance for the wave manager 
    public static waveManager instance;

    //Variable for the zombie spawner
    public zombieSpawn[] spawners;

    //Variable for the time between waves
    [SerializeField] int timeBetweenWaves;

    //Variable for current wave
    public int currentWave;

    // Start is called before the first frame update
    //Change start for swake
    void Awake()
    {
        //Instantiate
        instance = this;

        StartCoroutine(startWave());
    }

    //Create IEnumerator to start wave
    public IEnumerator startWave()
    {
        //Increase the current wave
        currentWave++;

        if(currentWave <= spawners.Length)
        {
            yield return new WaitForSeconds(timeBetweenWaves);

            //spawners[currentWave - 1].startWave();
        }
    }
}
