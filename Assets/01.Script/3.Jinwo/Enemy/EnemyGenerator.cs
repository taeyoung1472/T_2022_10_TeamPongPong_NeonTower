using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class EnemyGenerator : MonoSingleTon<EnemyGenerator>
{
    public bool isCanGenerated = true;

    [SerializeField] private EnemySpawnTable[] spawnTables;

    public GameObject GetEnemy()
    {
        GameObject returnObj = null;
        int maxRand = 0;
        foreach (var spawn in spawnTables)
        {
            maxRand += spawn.randValue[WaveManager.Instance.CurWave];
        }

        int rand = Random.Range(0, maxRand);
        int stack = 0;
        foreach (var spawn in spawnTables)
        {
            stack += spawn.randValue[WaveManager.Instance.CurWave];
            if (rand < stack)
            {
                returnObj = spawn.prefab;
                break;
            }
        }
        return returnObj;
    }

    [Serializable]
    class EnemySpawnTable
    {
        public string name;
        public GameObject prefab;
        public int[] randValue;
    }
}