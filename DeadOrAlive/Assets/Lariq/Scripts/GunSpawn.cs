using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpawn : MonoBehaviour
{
    //all weapons ij the game
    [SerializeField] private weaponStats[] weaponItem;

    //position where the gun gets spawned
    [SerializeField] private Transform[] spawnPos;

    // time till next gun spawns
    [SerializeField] private float spawnInterval;

    // calculates the time that passed
    private float spawnTimer = 0f;

    // checks how many weapons are spawned 
    private int numSpawned = 0;

    // max number of gun that could be spawned
    private int maxNumToSpawn = 5;
    // Start is called before the first frame update
    void Start()
    {
        //spawns a gun at start of game
        SpawnGuns();
    }

    // Update is called once per frame
    void Update()
    {
        // makes sure number over guns spawned doesnt go over
        if (numSpawned >= maxNumToSpawn)
            return;

        //sees how much time has passed
        spawnTimer += Time.deltaTime;

        //checks if timer is greater than time till spawn
        if (spawnTimer >= spawnInterval)
        {
            //if not spawns gun
            SpawnGuns();

            // resets time to cycle
            spawnTimer = 0f;
        }
    }
    private void SpawnGuns()
    {
        // goes through each spawn position
        foreach (Transform spawnPosition in spawnPos)
        {
            // randomly picks gun
            int index = Random.Range(0, weaponItem.Length);

            // sets the random gun to the given gun randomly chose
            weaponStats randomGun = weaponItem[index];

            //spawns the gun
            Instantiate(randomGun.itemModel, spawnPosition.position, spawnPosition.rotation);

            // increments as weapon is spawned
            numSpawned++;
        }
    }
}
