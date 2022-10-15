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
        stateMachineOwnerClass.attackCoolTime = stateMachineOwnerClass.Data.patternCoolTime;
    }

    public override void Execute()
    {

    }

    public override void Exit()
    {
        agent.isStopped = true;
    }

}
