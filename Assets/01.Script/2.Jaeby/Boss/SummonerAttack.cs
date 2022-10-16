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
        if (stateMachineOwnerClass.SlowCooltime > stateMachineOwnerClass.AttackDataSO._slowAttackCololtime)
        {
            stateMachine.ChangeState<SummonerSkillSlow>();
        }
        else if (stateMachineOwnerClass.SlowCooltime > stateMachineOwnerClass.AttackDataSO._summonAttackCooltime)
        {
            stateMachine.ChangeState<SummonerSkillSummon>();
        }

        Vector3 targetPos = stateMachineOwnerClass.Target.position;
        targetPos.y = stateMachineOwnerClass.transform.position.y;
        if (Vector3.Distance(targetPos, stateMachineOwnerClass.transform.position) < stateMachineOwnerClass.AttackDataSO._laserAttackDistance)
        {
            stateMachine.ChangeState<SummonerSkillLaser>();
        }
        /*else if (Vector3.Distance(targetPos, stateMachineOwnerClass.transform.position) < stateMachineOwnerClass.AttackDataSO._slowAttackDistance)
        {
            stateMachine.ChangeState<SummonerSkillSlow>();
        }
        else if (Vector3.Distance(targetPos, stateMachineOwnerClass.transform.position) < stateMachineOwnerClass.AttackDataSO._summonAttackDistance)
        {
            stateMachine.ChangeState<SummonerSkillSummon>();
        }*/
    }

    public override void Exit()
    {
    }
}
