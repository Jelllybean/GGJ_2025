using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public EnemyWave[] waves;
    public int startingWave;
    public Transform[] spawnLocations;
    public EnemyWaveSpawnMode spawnMode;
    public bool reachedEndOfWaves { get; private set; }

    public float minScale = 0.8f, maxScale = 1.2f;
    
    private EnemyWave currentWave;
    private int currentWaveIndex;
    private float lastWaveTime;
    private int totalWeights;

    private void Awake()
    {
        SetSettings(startingWave);
    }

    public void Update()
    {
        // don't go any further if no waves
        if (reachedEndOfWaves){ return;}
        
        // check for time delay
        if (lastWaveTime + currentWave.spawnWaveDelaySeconds < Time.time)
        {
            SpawnWave(currentWaveIndex);
            currentWaveIndex++;
            
            // set time
            lastWaveTime = Time.time;
        }
    }

    public void SpawnWave(int index)
    {
        int roundRobinCounter = 0;
        if (!SetSettings(index))
        {
            // reached end of waves
            reachedEndOfWaves = true;
            Debug.Log("No more waves");
            return;
        }
            
        // spawn an amount of enemies, each one at random based on the weights
        for (int i = 0; i < currentWave.spawnWaveAmount; i++)
        {
            GameObject enemyToSpawn = GetRandomEnemy();
            if (enemyToSpawn == null)
            {
                Debug.LogError("Could not find an enemy to spawn!");
                return;
            }
                
            // get spawn position based on spawnmode
            GameObject spawned = null;
            switch (spawnMode)
            {
                // RANDOM case
                default:
                    int rnd = Random.Range(0, spawnLocations.Length);
                    spawned = Instantiate(enemyToSpawn, spawnLocations[rnd].position, Quaternion.identity);
                    break;
                    
                case EnemyWaveSpawnMode.ROUND_ROBIN:
                    spawned = Instantiate(enemyToSpawn, spawnLocations[roundRobinCounter].position,
                        Quaternion.identity);
                    roundRobinCounter++;
                    roundRobinCounter %= spawnLocations.Length;
                    break;
                    
                case EnemyWaveSpawnMode.SAME_FOR_EACH:
                    for (int j = 0; j < spawnLocations.Length; j++)
                    {
                        spawned = Instantiate(enemyToSpawn, spawnLocations[j].position, Quaternion.identity);
                    }
                    break;
            }

            spawned.transform.localScale *= Random.Range(minScale, maxScale);
        }
    }
    
    public bool SetSettings(int index)
    {
        if (waves.Length <= index)
        {
            Debug.LogWarning($"EnemySpawner \"{transform.name}\" has no wave at index {currentWaveIndex}");
            return false;
        }
        
        currentWaveIndex = index;
        currentWave = waves[index];
        totalWeights = 0;
        
        // get the total weight of all enemies
        for (int i = 0; i < currentWave.entries.Length; i++)
        {
            totalWeights += currentWave.entries[i].weight;
        }

        return true;
    }

    public GameObject GetRandomEnemy()
    {
        // tally the weights of the currently checked
        int rnd = Random.Range(0, totalWeights);
        int weightAccumulated = 0;
        GameObject enemy = null;

        for (int i = 0; i < currentWave.entries.Length; i++)
        {
            // if accumulated weight ends up higher than rnd, select and break
            EnemyWaveEntry iWave = currentWave.entries[i];
            weightAccumulated += iWave.weight;

            if (weightAccumulated >= rnd)
            {
                enemy = iWave.prefab;
                break;
            }
        }

        return enemy;
    }

    public enum EnemyWaveSpawnMode
    {
        RANDOM,
        ROUND_ROBIN,
        SAME_FOR_EACH
    }
}
