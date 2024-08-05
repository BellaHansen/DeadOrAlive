using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawn : MonoBehaviour
{
    [SerializeField] Transform[] spawnPos;
    [SerializeField] List<GameObject> zombieModel;
    [SerializeField] float timeBetweenWaves;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = timeBetweenWaves;
        StartCoroutine(startSpawning());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator startSpawning()
    {
        while (true)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                SpawnZombies();
                timer = timeBetweenWaves; // Reset timer
            }
            yield return null;
        }
    }

    void SpawnZombies()
    {
        foreach (Transform spawn in spawnPos)
        {
            Instantiate(RandomZombie(), spawn.position, spawn.rotation);
        }
    }

    GameObject RandomZombie()
    {
        int index = Random.Range(0, zombieModel.Count);
        return zombieModel[index];
    }

}
