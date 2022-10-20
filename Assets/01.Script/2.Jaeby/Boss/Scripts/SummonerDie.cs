using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerDie : BossState<SummonerBoss>
{
    public override void Enter()
    {
        stateMachineOwnerClass.Agent.velocity = Vector3.zero;
        stateMachineOwnerClass.Agent.enabled = false;
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
    }
}