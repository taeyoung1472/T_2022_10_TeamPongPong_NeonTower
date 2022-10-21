using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die_RushBoss<T> : BossState<RushBoss> where T : BossBase<T>
{
    public override void Enter()
    {
        stateMachineOwnerClass.Col.enabled = false;
        stateMachineOwnerClass.Agent.enabled = false;
        stateMachineOwnerClass.Die();
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
    }
}
