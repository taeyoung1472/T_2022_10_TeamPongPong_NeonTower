using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SummonerWalk : BossState<SummonerBoss>
{
    private NavMeshAgent _agent = null;
    private Vector3 _targetPos = Vector3.zero;

    private readonly string _walkHash = "Walk";

    public override void OnAwake()
    {
        base.OnAwake();
        _agent = stateMachineOwnerClass.Agent;
    }

    public override void Enter()
    {
        Debug.Log("Walk State");
        BossUIManager.Instance.BossPopupText("������ �����Դϴ�", 0.5f, 0);
        stateMachineOwnerClass.Animator.SetBool(_walkHash, true);
        _agent.SetDestination(stateMachineOwnerClass.Target.position);
        stateMachineOwnerClass.TargetLook();
    }

    public override void Execute()
    {
        stateMachineOwnerClass.TargetLook();
        if (stateMachineOwnerClass.Target != null)
        {
            if (stateMachineOwnerClass.SlowCooltime > stateMachineOwnerClass.AttackDataSO.slowAttackCololtime)
            {
                stateMachine.ChangeState<SummonerSkillSlow>();
            }
            else if (stateMachineOwnerClass.SummonCooltime > stateMachineOwnerClass.AttackDataSO.summonAttackCooltime)
            {
                stateMachine.ChangeState<SummonerSkillSummon>();
            }

            _agent.SetDestination(stateMachineOwnerClass.Target.position);

            _targetPos = _agent.destination;
            _targetPos.y = stateMachineOwnerClass.transform.position.y;
            if(Vector3.Distance(stateMachineOwnerClass.transform.position, _targetPos) < stateMachineOwnerClass.AttackDataSO.keepDistance)
            {
                stateMachine.ChangeState<SummonerIdle>();
            }
        }
    }

    public override void Exit()
    {
        stateMachineOwnerClass.Animator.SetBool(_walkHash, false);
        stateMachineOwnerClass.ModelReset();

        _agent.ResetPath();
    }

}
