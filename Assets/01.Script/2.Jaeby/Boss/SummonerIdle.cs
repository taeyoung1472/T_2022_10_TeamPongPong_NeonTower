using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerIdle : BossState<SummonerBoss>
{
    private float _randomIdleTime = 0f;
    private Vector3 _targetPos = Vector3.zero;

    public override void Enter()
    {
        Debug.Log("Idle State");
        _randomIdleTime = Random.Range(0.3f, 0.5f);
        stateMachineOwnerClass.TargetLook();
        //stateMachineOwnerClass.Animator.transform.rotation =
        //    Quaternion.LookRotation((stateMachineOwnerClass.transform.position -
        //    stateMachineOwnerClass.Target.position).normalized);
    }


    public override void Execute()
    {
        stateMachineOwnerClass.TargetLook();

        if (stateMachine.GetStateDurationTime > _randomIdleTime)
        {
            if (stateMachineOwnerClass.Target != null)
            {
                _targetPos = stateMachineOwnerClass.Target.position;
                _targetPos.y = stateMachineOwnerClass.transform.position.y;
                if (Vector3.Distance(stateMachineOwnerClass.transform.position, _targetPos) < stateMachineOwnerClass.AttackDataSO._laserAttackDistance)
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
