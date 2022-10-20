using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Boss/Summoner/AttackData")]
public class SummonerAttackDataSO : ScriptableObject
{
    [Header("아이들 스테이트로 전환되는 거리")]
    public float keepDistance = 4f;
    [Header("아이들 스테이트 유지 시간")]
    public Vector2 randomIdleTime = new Vector2(0.2f, 0.5f);

    [Header("레이저 패턴 사용 거리")]
    public float laserAttackDistance = 5f;
    [Header("레이저 Arc 모양 크기")]
    public float laserArcSize = 6f;
    [Header("레이저 전조 기간")]
    public float laserAttackDangerInterval = 0.2f;

    [Header("스폰 패턴 쿨타임")]
    public float summonAttackCooltime = 4f;
    [Header("스포너 생성 최대 거리")]
    public float spawnerRandomCircle = 5f;
    [Header("장판 패턴 쿨타임")]
    public float slowAttackCololtime = 6f;
    [Header("장판 지속시간")]
    public float slowAttackDuration = 6f;
    [Header("장판 슬로우 비율")]
    [Range(0.1f, 1f)]
    public float slowIntensity = 0.5f;
    [Header("장판이 플레이어한테 바로 꽃히게 할 것인지")]
    public bool immPlayer = false;
    [Header("장판 생성 최대 거리")]
    public float slowRandomCircle = 4f;
    [Header("장판 크기")]
    public float slowScale = 8f;

    [Header("적 생성 개수")]
    public int enemySpawnCount = 5;
    [Header("적 스폰 딜레이")]
    public float enemySpawnDelay = 1f;

    [Space(30)]
    [Header("오브젝트")]
    public GameObject slowClrcle = null;
    public GameObject enemySpawner = null;
    public List<PoolType> spawnableEnemys = null;
}
