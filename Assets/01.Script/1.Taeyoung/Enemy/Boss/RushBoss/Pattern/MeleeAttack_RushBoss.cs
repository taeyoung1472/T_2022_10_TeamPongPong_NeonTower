using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MeleeAttack_RushBoss<T> : BossState<RushBoss> where T : BossBase<T>
{
    public override void Enter()
    {
        stateMachineOwnerClass.ModelReset();
        stateMachineOwnerClass.TargetLook();
        stateMachineOwnerClass.Agent.ResetPath();
        stateMachineOwnerClass.Agent.isStopped = true;
        stateMachineOwnerClass.Animator.SetTrigger("Punch");
    }

    public override void Execute()
    {

    }

    public override void Exit()
    {
        stateMachineOwnerClass.Agent.isStopped = false;
        stateMachineOwnerClass.ModelReset();
    }
}
