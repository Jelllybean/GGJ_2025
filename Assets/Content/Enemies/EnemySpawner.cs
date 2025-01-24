using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public EnemySpawnerSettings selectedSettings;

    private float lastWaveTime;
    private int totalWeights;

    public void SetSettings(EnemySpawnerSettings newSettings)
    {
        selectedSettings = newSettings;
        totalWeights = 0;
        
        // get the total weight of all enemies
        for (int i = 0; i < selectedSettings.entries.Length; i++)
        {
            totalWeights += selectedSettings.entries[i].weight;
        }
    }

    public void Update()
    {
        EnemySpawnerSettings settings = selectedSettings;
        
        // don't go any further if no fallback
        if (settings == null)
        {
            Debug.LogError($"EnemySpawner \"{transform.name}\" has no settings!!");
            return;
        }
        
        // check for time delay
        if (lastWaveTime + settings.spawnWaveDelaySeconds < Time.time)
        {
            // spawn an amount of enemies, each one at random based on the weights
            for (int i = 0; i < settings.spawnWaveAmount; i++)
            {
                GameObject enemyToSpawn = GetRandomEnemy();
                // TODO: add spawn radius
                Instantiate(enemyToSpawn);
            }
            
            // set time
            lastWaveTime = Time.time;
        }
    }

    public GameObject GetRandomEnemy()
    {
        // tally the weights of the currently checked
        int rnd = Random.Range(0, totalWeights);
        int weightAccumulated = 0;
        GameObject enemy = null;

        for (int i = 0; i < selectedSettings.entries.Length; i++)
        {
            // if accumulated weight ends up higher than rnd, select and break
            EnemySpawnerSettingsEntry iSetting = selectedSettings.entries[i];
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
