using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieSpawn : MonoBehaviour
{
    [SerializeField] int spawnCount;
    [SerializeField] float spawnDelay;
    [SerializeField] int spawnIncrease;
    [SerializeField] GameObject zombie;

    bool isWaiting;

    Vector3 spawnPosition;
    Quaternion spawnRotation;

    // Start is called before the first frame update
    void Start()
    {
        isWaiting = false;
        spawnPosition = transform.position;
        spawnRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isWaiting)
        {
            StartCoroutine(spawnWave());
        }
    }
    IEnumerator spawnWave()
    {
        isWaiting = true;
        for (int i = 0; i < spawnCount; i++)
        {
            Instantiate(zombie, spawnPosition, spawnRotation);
        }
        spawnCount += spawnIncrease;
        yield return new WaitForSeconds(spawnDelay);
        isWaiting = false;
    }
}
