using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Boss/Rush/AttackData")]
public class RushBossAttackDataSO : ScriptableObject
{
    [Header("아이들 랜덤 지속 시간")]
    public Vector2 randomIdleTime;
    [Header("기본 이속")]
    public float normalSpeed = 8f;
    [Header("돌진 속도")]
    public float rushSpeed = 16f;

    [Header("근접 범위 공격 넘어가는 거리")]
    public float attackDistance = 5f;
    [Header("범위공격 사이즈")]
    public float groundPoundSize = 2f;
    [Header("웨이브 어택 개당 사이즈")]
    public float waveAttackSize = 6f;

    [Header("돌진 지속시간")]
    public float rushDuration = 3f;
    [Header("돌진으로 넘어가는 최소 거리")]
    public float rushDistance = 15f;

    [Header("점프 패턴에서 멈춰있는 시간")]
    public float jumpidleTime = 1.2f;
    [Header("점프 패턴에서 떨어지는 시간")]
    public float fallTime = 0.5f;

    [Header("근접 범위 공격 대기 시간")]
    public float[] punchDelays = new float[3];
}
