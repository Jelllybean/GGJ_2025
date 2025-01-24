using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public EnemyWave[] waves;
    public int startingWave;
    
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
        if (waves.Length <= currentWaveIndex)
        {
            Debug.LogWarning($"EnemySpawner \"{transform.name}\" has no wave {currentWaveIndex}");
            return;
        }
        
        // check for time delay
        if (lastWaveTime + currentWave.spawnWaveDelaySeconds < Time.time)
        {
            // spawn an amount of enemies, each one at random based on the weights
            for (int i = 0; i < currentWave.spawnWaveAmount; i++)
            {
                GameObject enemyToSpawn = GetRandomEnemy();
                // TODO: add spawnpoints
                Instantiate(enemyToSpawn);
            }
            
            // set time
            lastWaveTime = Time.time;
            currentWaveIndex++;
            SetSettings(currentWaveIndex);
        }
    }
    
    public void SetSettings(int index)
    {
        currentWaveIndex = index;
        currentWave = waves[index];
        totalWeights = 0;
        
        // get the total weight of all enemies
        for (int i = 0; i < currentWave.entries.Length; i++)
        {
            totalWeights += currentWave.entries[i].weight;
        }
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
            EnemyWaveEntry iSetting = currentWave.entries[i];
            weightAccumulated += iSetting.weight;

            if (weightAccumulated <= rnd)
            {
                enemy = iSetting.prefab;
                break;
            }
        }

        return enemy;
    }
}
