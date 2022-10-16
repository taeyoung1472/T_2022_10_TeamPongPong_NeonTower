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
        BossUIManager.Instance.BossPopupText("보스가 움직입니다", 0.5f, 0);
        stateMachineOwnerClass.Animator.SetBool(_walkHash, true);
        _agent.SetDestination(stateMachineOwnerClass.Target.position);
        stateMachineOwnerClass.TargetLook();
    }

    public override void Execute()
    {
        stateMachineOwnerClass.TargetLook();
        if (stateMachineOwnerClass.Target != null)
        {
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
        //Vector3 eu = stateMachineOwnerClass.Animator.transform.rotation.eulerAngles;
        //stateMachineOwnerClass.Animator.transform.rotation = Quaternion.Euler(eu.x, eu.y, 0f);
        stateMachineOwnerClass.ModelReset();

        _agent.ResetPath();
    }

}
