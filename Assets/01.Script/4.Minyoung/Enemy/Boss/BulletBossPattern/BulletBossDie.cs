using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBossDie : BossState<BulletBoss>
{
    public override void Enter()
    {
        stateMachineOwnerClass.Agent.velocity = Vector3.zero;
        stateMachineOwnerClass.Agent.enabled = false;
        stateMachineOwnerClass.BulletBossDieEffect();
        stateMachineOwnerClass.Die();
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
    }
}
