using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerDie : BossState<SummonerBoss>
{
    public override void Enter()
    {
        EnemySubject.Instance.NotifyObserver();
        stateMachineOwnerClass.Agent.velocity = Vector3.zero;
        stateMachineOwnerClass.Agent.enabled = false;
        stateMachineOwnerClass.SummonDieEffect();
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
    }
}
