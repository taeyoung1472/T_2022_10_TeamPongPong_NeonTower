using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Idle_RushBoss<T> : BossState<RushBoss> where T : BossBase<T>
{
    float _randomTime = 0f;

    public override void Enter()
    {
        stateMachineOwnerClass.ModelReset();
        _randomTime = Random.Range(stateMachineOwnerClass.AttackDataSO.randomIdleTime.x,
            stateMachineOwnerClass.AttackDataSO.randomIdleTime.y);
    }

    public override void Execute()
    {
        Vector3 tar = stateMachineOwnerClass.Target.position;
        tar.y = stateMachineOwnerClass.transform.position.y;
        if(Vector3.Distance(stateMachineOwnerClass.transform.position, tar) < stateMachineOwnerClass.AttackDataSO.attackDistance)
        {

        }

        if (stateMachine.GetStateDurationTime > _randomTime)
        {
            stateMachine.ChangeState<Move_RushBoss<RushBoss>>();
        }
    }

    public override void Exit()
    {
        stateMachineOwnerClass.ModelReset();
    }
}
