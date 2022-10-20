using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Boss/Rush/AttackData")]
public class RushBossAttackDataSO : ScriptableObject
{
    [Header("���̵� ���� ���� �ð�")]
    public Vector2 randomIdleTime;
    public float normalSpeed = 8f;
    public float rushSpeed = 16f;

    public float attackDistance = 5f;
    public float groundPoundSize = 2f;
    public float waveAttackSize = 6f;

    public float rushDuration = 3f;
    public float rushDistance = 15f;

    public float[] punchDelays = new float[3];
}
