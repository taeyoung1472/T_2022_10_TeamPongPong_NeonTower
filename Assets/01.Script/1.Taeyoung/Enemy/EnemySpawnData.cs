using System;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/EnemySpawnTable")]
public class EnemySpawnData : ScriptableObject
{
    public SpawnTable[] spawnTable;
    public float[] spawnDelay;

    [Serializable]
    public class SpawnTable
    {
        public PoolType enemyType;
        public int[] spawnChance;
    }
}
