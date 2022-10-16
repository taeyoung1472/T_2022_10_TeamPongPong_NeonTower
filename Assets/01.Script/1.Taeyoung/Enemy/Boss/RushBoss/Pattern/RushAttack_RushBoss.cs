using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RushAttack_RushBoss<T> : BossState<RushBoss> where T : BossBase<T>
{
    Transform transform;
    public override void Enter()
    {
        transform = stateMachineOwnerClass.transform;
        stateMachineOwnerClass.Agent.speed = 0;
        stateMachineOwnerClass.LookTarget();
        stateMachineOwnerClass.Agent.SetDestination(stateMachineOwnerClass.Target.position);
        DangerZone.DrawBox(transform.position + transform.forward * 50, transform.rotation, new Vector3(3, 1, 100), 1);
    }

    public override void Execute()
    {
        if(stateMachine.GetStateDurationTime > 1)
        {
            stateMachineOwnerClass.MovementGoal = 1;
            stateMachineOwnerClass.Agent.speed = stateMachineOwnerClass.rushSpeed;
        }

        if (stateMachine.GetStateDurationTime > 1 && stateMachine.GetStateDurationTime > 4)
        {
            stateMachineOwnerClass.Agent.speed = stateMachineOwnerClass.defaultSpeed;
            stateMachine.ChangeState<Idle_RushBoss<RushBoss>>();
        }
    }

    public override void Exit()
    {

    }
}
