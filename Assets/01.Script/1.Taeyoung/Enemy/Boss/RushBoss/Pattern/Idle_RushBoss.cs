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
        Debug.Log("¾ÆÀÌµé");
        stateMachineOwnerClass.ModelReset();
        _randomTime = Random.Range(stateMachineOwnerClass.AttackDataSO.randomIdleTime.x,
            stateMachineOwnerClass.AttackDataSO.randomIdleTime.y);
    }

    public override void Execute()
    {

        if (stateMachine.GetStateDurationTime > _randomTime)
        {
            if (stateMachineOwnerClass.GetDistance() < stateMachineOwnerClass.AttackDataSO.attackDistance)
            {
                stateMachine.ChangeState<MeleeAttack_RushBoss<RushBoss>>();
            }
            else
            {
                stateMachine.ChangeState<Move_RushBoss<RushBoss>>();
            }
        }
    }

    public override void Exit()
    {
        stateMachineOwnerClass.ModelReset();
    }
}
