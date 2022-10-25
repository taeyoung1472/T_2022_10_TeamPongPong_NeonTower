using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerIdle : BossState<SummonerBoss>
{
    private Vector3 _targetPos = Vector3.zero;

    public override void Enter()
    {
        Debug.Log("Idle State");
        stateMachineOwnerClass.TargetLook();
    }


    public override void Execute()
    {
        stateMachineOwnerClass.TargetLook();

        if (stateMachine.GetStateDurationTime > stateMachineOwnerClass.Data.patternCoolTime[Define.Instance.Difficulty])
        {


            if (stateMachineOwnerClass.Target != null)
            {
                _targetPos = stateMachineOwnerClass.Target.position;
                _targetPos.y = stateMachineOwnerClass.transform.position.y;
                if (Vector3.Distance(stateMachineOwnerClass.transform.position, _targetPos) < stateMachineOwnerClass.AttackDataSO.laserAttackDistance)
                {
                    stateMachine.ChangeState<SummonerAttack>();
                }
                else
                {
                    stateMachine.ChangeState<SummonerWalk>();
                }
            }
        }
    }

    public override void Exit()
    {
        stateMachineOwnerClass.TargetLook();
    }
}
