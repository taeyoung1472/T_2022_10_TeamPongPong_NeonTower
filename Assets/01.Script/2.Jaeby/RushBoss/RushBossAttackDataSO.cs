using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Boss/Rush/AttackData")]
public class RushBossAttackDataSO : ScriptableObject
{
    [Header("아이들 랜덤 지속 시간")]
    public Vector2 randomIdleTime;
    public float normalSpeed = 8f;
    public float rushSpeed = 16f;

    public float attackDistance = 5f;
    public float groundPoundDistance = 2f;
    public float waveAttackDistance = 8f;

    public float[] punchDelays = new float[3];
}
