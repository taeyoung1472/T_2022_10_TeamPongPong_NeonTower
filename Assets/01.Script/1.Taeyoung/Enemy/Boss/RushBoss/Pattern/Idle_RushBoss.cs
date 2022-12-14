using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Idle_RushBoss<T> : BossState<RushBoss> where T : BossBase<T>
{
    public override void Enter()
    {
        stateMachineOwnerClass.After.isMotionTrail = false;

        Debug.Log("???̵?");
        stateMachineOwnerClass.ModelReset();
    }

    public override void Execute()
    {

        if (stateMachine.GetStateDurationTime > stateMachineOwnerClass.Data.patternCoolTime[Define.Instance.Difficulty])
        {
            if (stateMachineOwnerClass.GetDistance() < stateMachineOwnerClass.AttackDataSO.attackDistance)
            {
                stateMachine.ChangeState<MeleeAttack_RushBoss<RushBoss>>();
            }
            else if (stateMachineOwnerClass.GetDistance() > stateMachineOwnerClass.AttackDataSO.rushDistance && stateMachineOwnerClass.IsFirst == false)
            {
                if(Random.Range(0,2) == 0)
                {
                    stateMachine.ChangeState<RushAttack_RushBoss<RushBoss>>();
                }
                else
                {
                    stateMachine.ChangeState<JumpAttack_RushBoss<RushBoss>>();
                }
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
