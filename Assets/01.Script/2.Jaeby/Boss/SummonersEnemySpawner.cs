using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SummonersEnemySpawner : MonoBehaviour
{
    private PoolType _spawnObj;
    public PoolType SpawnObj
    {
        get => _spawnObj;
        set => _spawnObj = value;
    }

    private GameObject _target = null;
    public GameObject Target
    {
        get => _target;
        set => _target = value;
    }

    private void Start()
    {
        Invoke("Spawn", 1f);
    }

    private void Spawn()
    {
        NavMeshHit hit;
        if(NavMesh.SamplePosition(transform.position, out hit, Mathf.Infinity, NavMesh.AllAreas))
        {
            Enemy obj = PoolManager.Instance.Pop(SpawnObj).GetComponent<Enemy>();
            obj.transform.position = new Vector3(hit.position.x, 1f, hit.position.z);
            obj.Target = _target;
        }
        Debug.Log(hit.position);
        Destroy(gameObject);
    }
}
