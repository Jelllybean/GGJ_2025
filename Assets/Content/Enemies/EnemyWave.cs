using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpawnerTable", menuName = "EnemySpawnerTable", order = -10000)]
public class EnemyWave : ScriptableObject
{
    public EnemyWaveEntry[] entries;
    public float spawnWaveDelaySeconds;
    public int spawnWaveAmount;
}

[System.Serializable]
public struct EnemyWaveEntry
{
    public GameObject prefab;
    public int weight;
}
