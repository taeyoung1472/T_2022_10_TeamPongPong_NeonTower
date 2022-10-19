using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateMeleeAttack<T> : State<T> where T: EnemyBase<T>
{
    private Animator animator;
    private Transform characterTransform;

    private NavMeshAgent agent;

    private int hashAttack = Animator.StringToHash("Attack");

    public override void OnAwake()
    {
        animator = stateMachineOwnerClass.GetComponent<Animator>();
        characterTransform = stateMachineOwnerClass.GetComponent<Transform>();

        agent = stateMachineOwnerClass.GetComponent<NavMeshAgent>();

    }
    public override void Enter()
    {
        stateMachineOwnerClass.lastAttackTime = Time.time;
        animator?.SetTrigger(hashAttack);
    }
    public override void Execute()
    {
        Transform target = stateMachineOwnerClass.Target.transform;

        if (Vector3.Distance(target.position, agent.transform.position) >
               stateMachineOwnerClass.EnemyData.attackDistance)
        {
            stateMachine.ChangeState<StateMove<T>>();
        }
    }
    public override void Exit()
    {

    }

    

}
