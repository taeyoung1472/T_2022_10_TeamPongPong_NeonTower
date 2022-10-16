using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Boss/Summoner/AttackData")]
public class SummonerAttackDataSO : ScriptableObject
{
    public float _laserAttackDistance = 5f;
    public float _summonAttackDistance = 4f;
    public float _slowAttackDistance = 6f;
}
