using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Boss/Summoner/AttackData")]
public class SummonerAttackDataSO : ScriptableObject
{
    public float _laserAttackDistance = 5f;
    public float _laserAttackDangerInterval = 0.2f;
    public float _summonAttackCooltime = 4f;
    public float _slowAttackCololtime = 6f;
}
