using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SummonerWalk : BossState<SummonerBoss>
{
    private NavMeshAgent _agent = null;
    private Vector3 _targetPos = Vector3.zero;

    public override void OnAwake()
    {
        base.OnAwake();
        _agent = stateMachineOwnerClass.GetComponent<NavMeshAgent>();
        _agent.updatePosition = true;
        _agent.updateRotation = true;
    }

    public override void Enter()
    {
        Debug.Log("Walk State");
        BossUIManager.Instance.BossPopupText("º¸½º°¡ ¿òÁ÷ÀÔ´Ï´Ù ¾¾´í", 0.5f);
        _agent.SetDestination(stateMachineOwnerClass.Target.position);
    }

    public override void Execute()
    {
        if (stateMachineOwnerClass.Target != null)
        {
            _agent.SetDestination(stateMachineOwnerClass.Target.position);

            _targetPos = _agent.destination;
            _targetPos.y = stateMachineOwnerClass.transform.position.y;
            if(Vector3.Distance(stateMachineOwnerClass.transform.position, _targetPos) < 1.5f)
            {
                stateMachine.ChangeState<SummonerIdle>();
            }
        }
    }

    public override void Exit()
    {
    }

}
