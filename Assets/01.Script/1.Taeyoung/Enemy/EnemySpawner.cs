using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemySpawnData;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemySpawnData spawnData;

    private List<Transform> spawnPosList = new();
    private int spawPosIndex = 0;
    private Transform playerTrans;

    private bool isCanSpawn = true;
    public bool IsCanSpawn { get { return isCanSpawn; } set { isCanSpawn = value; } }

    public void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            spawnPosList.Add(transform.GetChild(i));
        }
        playerTrans = FindObjectOfType<PlayerController>().transform;
        StartCoroutine(SpawnSystem());
    }

    private IEnumerator SpawnSystem()
    {
        while (true)
        {
            yield return new WaitUntil(() => IsCanSpawn);

            PoolType generatedType = GenerateEnemy();

            Enemy enemy = PoolManager.Instance.Pop(generatedType) as Enemy;
            enemy.Init(spawnPosList[spawPosIndex].position, playerTrans.gameObject);

            spawPosIndex = (spawPosIndex + 1) % spawnPosList.Count;

            yield return new WaitForSeconds(spawnData.spawnDelay[WaveManager.Instance.CurWave]);
        }
    }

    private PoolType GenerateEnemy()
    {
        PoolType returnType = PoolType.ComonEnemy;
        int maxRand = 0;

        foreach (var table in spawnData.spawnTable)
        {
            maxRand += table.spawnChance[WaveManager.Instance.CurWave];
        }

        int rand = Random.Range(0, maxRand);
        int stack = 0;
        foreach (var spawn in spawnData.spawnTable)
        {
            stack += spawn.spawnChance[WaveManager.Instance.CurWave];
            if (rand < stack)
            {
                returnType = spawn.enemyType;
                break;
            }
        }
        return returnType;
    }
}
