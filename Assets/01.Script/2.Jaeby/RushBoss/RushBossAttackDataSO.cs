using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Boss/Rush/AttackData")]
public class RushBossAttackDataSO : ScriptableObject
{
    [Header("���̵� ���� ���� �ð�")]
    public Vector2 randomIdleTime;
    [Header("�⺻ �̼�")]
    public float normalSpeed = 8f;
    [Header("���� �ӵ�")]
    public float rushSpeed = 16f;

    [Header("���� ���� ���� �Ѿ�� �Ÿ�")]
    public float attackDistance = 5f;
    [Header("�������� ������")]
    public float groundPoundSize = 2f;
    [Header("���̺� ���� ���� ������")]
    public float waveAttackSize = 6f;

    [Header("���� ���ӽð�")]
    public float rushDuration = 3f;
    [Header("�������� �Ѿ�� �ּ� �Ÿ�")]
    public float rushDistance = 15f;

    [Header("���� ���Ͽ��� �����ִ� �ð�")]
    public float jumpidleTime = 1.2f;
    [Header("���� ���Ͽ��� �������� �ð�")]
    public float fallTime = 0.5f;

    [Header("���� ���� ���� ��� �ð�")]
    public float[] punchDelays = new float[3];
}
