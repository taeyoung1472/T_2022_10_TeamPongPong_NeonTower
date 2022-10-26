using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SwordIdle<T> : BossState<T> where T : Sword
{
    private Animator animator;

    private NavMeshAgent agent;

    
    private int hashMove = Animator.StringToHash("Move");
    public override void OnAwake()
    {
        animator = stateMachineOwnerClass.Animator;

        agent = stateMachineOwnerClass.Agent;

        agent.isStopped = true;

    }
    public override void Enter()
    {
        agent.speed = 0;
        stateMachineOwnerClass.attackCoolTime = 0;
    }

    public override void Execute()
    {

        stateMachineOwnerClass.attackCoolTime += Time.deltaTime;
        if (stateMachineOwnerClass.attackCoolTime >= stateMachineOwnerClass.Data.patternCoolTime[Define.Instance.Difficulty])
        // ÄðÅ¸ÀÓ ´Ùµ¼
        {
            agent.isStopped = false;
            //stateMachine.ChangeState<SwordDie<T>>();
            stateMachine.ChangeState<SwordMove<T>>();
        }
    }

    public override void Exit()
    {
        
    }

}
