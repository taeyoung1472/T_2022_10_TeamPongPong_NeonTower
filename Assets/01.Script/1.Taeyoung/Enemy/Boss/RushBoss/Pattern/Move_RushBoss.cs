using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class Move_RushBoss<T> : BossState<RushBoss> where T : BossBase<T>
{
    Vector3 goalPoint;
    Transform transform;
    public override void Enter()
    {
        stateMachineOwnerClass.Agent.enabled = true;
        goalPoint = stateMachineOwnerClass.Target.position;
        transform = stateMachineOwnerClass.transform;
        stateMachineOwnerClass.Agent.SetDestination(goalPoint);
    }

    public override void Execute()
    {
        if(Vector3.Distance(transform.position, goalPoint) < 1)
        {
            stateMachine.ChangeState<Idle_RushBoss<RushBoss>>();
        }
    }

    public override void Exit()
    {
        stateMachineOwnerClass.Agent.enabled = false;
    }
}
