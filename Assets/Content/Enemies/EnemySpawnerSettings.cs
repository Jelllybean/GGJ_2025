using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpawnerTable", menuName = "EnemySpawnerTable", order = -10000)]
public class EnemySpawnerSettings : ScriptableObject
{
    public EnemySpawnerSettingsEntry[] entries;
    public float spawnWaveDelaySeconds;
    public int spawnWaveAmount;
}

[System.Serializable]
public struct EnemySpawnerSettingsEntry
{
    public GameObject prefab;
    public int weight;
}
