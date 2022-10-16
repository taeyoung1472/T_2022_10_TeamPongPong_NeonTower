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
    [Header("������ ���� �Ⱓ")]
    public float laserAttackDangerInterval = 0.2f;
    [Header("������ ����")]
    public float laserLength = 5f;

    [Header("���� ���� ��Ÿ��")]
    public float summonAttackCooltime = 4f;
    [Header("������ ���� �ִ� �Ÿ�")]
    public float spawnerRandomCircle = 5f;
    [Header("���� ���� ��Ÿ��")]
    public float slowAttackCololtime = 6f;
    [Header("���� ���� �ִ� �Ÿ�")]
    public float slowRandomCircle = 4f;

    [Space(30)]
    [Header("������Ʈ")]
    public GameObject spawner = null;
    public GameObject slowClrcle = null;
}
