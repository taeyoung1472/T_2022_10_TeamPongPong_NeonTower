using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Boss/Summoner/AttackData")]
public class SummonerAttackDataSO : ScriptableObject
{
    [Header("���̵� ������Ʈ�� ��ȯ�Ǵ� �Ÿ�")]
    public float keepDistance = 4f;
    [Header("���̵� ������Ʈ ���� �ð�")]
    public Vector2 randomIdleTime = new Vector2(0.2f, 0.5f);

    [Header("������ ���� ��� �Ÿ�")]
    public float laserAttackDistance = 5f;
    [Header("������ Arc ��� ũ��")]
    public float laserArcSize = 6f;
    [Header("������ ���� �Ⱓ")]
    public float laserAttackDangerInterval = 0.2f;

    [Header("���� ���� ��Ÿ��")]
    public float summonAttackCooltime = 4f;
    [Header("������ ���� �ִ� �Ÿ�")]
    public float spawnerRandomCircle = 5f;
    [Header("���� ���� ��Ÿ��")]
    public float slowAttackCololtime = 6f;
    [Header("���� ���ӽð�")]
    public float slowAttackDuration = 6f;
    [Header("���� ���ο� ����")]
    [Range(0.1f, 1f)]
    public float slowIntensity = 0.5f;
    [Header("������ �÷��̾����� �ٷ� ������ �� ������")]
    public bool immPlayer = false;
    [Header("���� ���� �ִ� �Ÿ�")]
    public float slowRandomCircle = 4f;
    [Header("���� ũ��")]
    public float slowScale = 8f;

    [Header("�� ���� ����")]
    public int enemySpawnCount = 5;
    [Header("�� ���� ������")]
    public float enemySpawnDelay = 1f;

    [Space(30)]
    [Header("������Ʈ")]
    public GameObject slowClrcle = null;
    public GameObject enemySpawner = null;
    public List<PoolType> spawnableEnemys = null;
}
