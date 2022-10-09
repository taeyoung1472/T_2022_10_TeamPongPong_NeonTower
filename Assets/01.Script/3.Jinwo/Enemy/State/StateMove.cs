using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateMove<T> : State<T> where T : EnemyBase
{
    private Animator animator;
    
    private NavMeshAgent agent;

    private int hashMove = Animator.StringToHash("Move");

    
    // 등록시 초기화
    public override void OnAwake()
    {
        animator = stateMachineOwnerClass.GetComponent<Animator>();

        agent = stateMachineOwnerClass.GetComponent<NavMeshAgent>();

        agent.stoppingDistance = stateMachineOwnerClass.EnemyData.stoppingDistance;

        agent.speed = stateMachineOwnerClass.EnemyData.maxSpeed;
    }

    public override void Enter()
    {
        animator?.SetBool(hashMove, true);
        agent?.SetDestination(stateMachineOwnerClass.Target.transform.position);

    }
    public override void Execute()
    {
        if(stateMachineOwnerClass.Health <= 0)
        {

        }
        else
        {
            if (agent.remainingDistance > agent.stoppingDistance)
            {

            }
        }
        
    }

    public override void Exit()
    {
        animator?.SetBool(hashMove, false);

        agent.ResetPath();
    }
}
