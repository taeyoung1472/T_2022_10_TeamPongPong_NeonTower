using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SwordMove<T> : BossState<T> where T : Sword
{

    private Animator animator;

    private NavMeshAgent agent;

    private int hashMove = Animator.StringToHash("Move");

    private Transform target;


    public override void OnAwake()
    {
        animator = stateMachineOwnerClass.GetComponentInChildren<Animator>();

        agent = stateMachineOwnerClass.GetComponent<NavMeshAgent>();

        //agent.stoppingDistance = 7f;

        agent.speed = stateMachineOwnerClass.Data.speed;
    }
    public override void Enter()
    {
        animator?.SetBool(hashMove, true);
        target = stateMachineOwnerClass.Target.transform;
        stateMachineOwnerClass.attackCoolTime = 0;
    }

    public override void Execute()
    {
        target = stateMachineOwnerClass.Target.transform;
        agent?.SetDestination(target.position);
        stateMachineOwnerClass.attackCoolTime += Time.deltaTime;

        if (Vector3.Distance(target.position, agent.transform.position) <=
               stateMachineOwnerClass.Data.attackRange && stateMachineOwnerClass.attackCoolTime >= stateMachineOwnerClass.Data.patternCoolTime) 
               // ���� �÷��̾ ��Ÿ���� �� �ư� ���� �����ȿ� ��� ������
        {
            stateMachineOwnerClass.ChangeAttack();

        }
    }

    public override void Exit()
    {
        animator?.SetBool(hashMove, false);

        agent.ResetPath();
    }

    
}
