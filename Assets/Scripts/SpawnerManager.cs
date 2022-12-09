using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SpawnerManager : MonoBehaviour
{

    // Variables
    [SerializeField] GameObject gameManager;
    [SerializeField] GameObject[] enemies;

    private bool spawn = true;
    private float spawnAmount = 2000f;
    private bool spawnerOn = true;

    // Getters and setters
    public bool Spawn
    {
        get { return spawn; }
        set { spawn = value; }
    }

    public bool SpawnerOn
    {
        get { return spawnerOn; }
        set { spawnerOn = value; }
    }

    public float SpawnAmount
    {
        get { return spawnAmount; }
        set { SpawnAmount = value; }
    }

    private void Update()
    {
        StartSpawning(); // Start spawning characters
    }

    private void StartSpawning()
    {
        if (spawn) // Check if spawn is true
        {
            if(spawnerOn) // if true, check if the spawner is working
            {
                StartCoroutine(Spawning()); // Start spawning
                spawnerOn = false; // Disable spawner so that only 1 instance is playing
            }
        }
        else
        {
            StopCoroutine(Spawning()); // Stop spawner
        }
    }

    IEnumerator Spawning()
    {
        // Get random enemy from array
        // Spawn random enemy
        // Loop X times
        // Stop after X times
        // Reset values

        for(; ; )
        {
            if (spawn) // Check if spawn is true
            { 
                if(spawnAmount > 0f) // Ensure that you can spawn more than 0 enemies
                {
                    GameObject enemy = GameObject.Instantiate(enemies[Random.Range(1, 5)]); // Spawn enemy
                    spawnAmount--; // Remove 1 from spawn amount
                    
                }else
                {
                    spawn = false; // Disable spawning
                }
            }

            yield return new WaitForSeconds(1.5f);
        }
    }
}
