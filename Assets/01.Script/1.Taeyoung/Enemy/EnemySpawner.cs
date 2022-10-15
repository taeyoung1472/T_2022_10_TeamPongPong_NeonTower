using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float[] spawnDelay;

    private List<Transform> spawnPosList = new();
    private int spawPosIndex;

    private bool isCanSpawn = true;
    public bool IsCanSpawn { get { return isCanSpawn; } set { isCanSpawn = value; } }

    public void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            spawnPosList.Add(transform.GetChild(i));
        }
        StartCoroutine(SpawnSystem());
    }

    private IEnumerator SpawnSystem()
    {
        while (true)
        {
            yield return new WaitUntil(() => IsCanSpawn);
            Debug.Log($"EnemySpawn At {spawnPosList[spawPosIndex].position}");
            spawPosIndex = (spawPosIndex + 1) % spawnPosList.Count;
            yield return new WaitForSeconds(spawnDelay[WaveManager.Instance.CurWave]);
        }
    }
}
