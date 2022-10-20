using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerModelAnimationEvent : MonoBehaviour
{
    [SerializeField]
    private Transform _parent = null;
    public void CheckArc()
    {
        List<Collider> cols =  EnemyAttackCollisionCheck.CheckArc(_parent, 60f, 5f, 1 << 8);
        EnemyAttackCollisionCheck.ApplyDamaged(cols, 1);
    }
}
