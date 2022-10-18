using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerAttack : BossState<SummonerBoss>
{

    public override void Enter()
    {
    }

    public override void Execute()
    {
        stateMachineOwnerClass.TargetLook();

        Vector3 targetPos = stateMachineOwnerClass.Target.position;
        targetPos.y = stateMachineOwnerClass.transform.position.y;

        if (stateMachineOwnerClass.SlowCooltime > stateMachineOwnerClass.AttackDataSO.slowAttackCololtime)
        {
            stateMachine.ChangeState<SummonerSkillSlow>();
        }
        else if (stateMachineOwnerClass.SummonCooltime > stateMachineOwnerClass.AttackDataSO.summonAttackCooltime)
        {
            stateMachine.ChangeState<SummonerSkillSummon>();
        }
        else if (Vector3.Distance(targetPos, stateMachineOwnerClass.transform.position) < stateMachineOwnerClass.AttackDataSO.laserAttackDistance)
        {
            stateMachine.ChangeState<SummonerSkillLaser>();
        }
    }

    public override void Exit()
    {
    }
}
