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
        animator = stateMachineOwnerClass.Animator;

        agent = stateMachineOwnerClass.Agent;

        //agent.stoppingDistance = 7f;

    }
    public override void Enter()
    {
        agent.speed = stateMachineOwnerClass.Data.speed;
        agent.isStopped = false;
        animator?.SetBool(hashMove, true);
        target = stateMachineOwnerClass.Target.transform;
        stateMachineOwnerClass.attackCoolTime = 0;
        stateMachineOwnerClass.motionTrail.isMotionTrail = true;
    }

    public override void Execute()
    {
        target = stateMachineOwnerClass.Target.transform;
        agent?.SetDestination(target.position);
        stateMachineOwnerClass.attackCoolTime += Time.deltaTime;

        if (Vector3.Distance(target.position, agent.transform.position) <=
               stateMachineOwnerClass.Data.attackRange && stateMachineOwnerClass.attackCoolTime >= stateMachineOwnerClass.Data.patternCoolTime) 
               // 만약 플레이어가 쿨타임이 다 됐고 공격 범위안에 들어 왔을시
        {
            agent.speed = 0;
            agent.isStopped = true;
            stateMachineOwnerClass.ChangeAttack();

        }
    }

    public override void Exit()
    {
        animator?.SetBool(hashMove, false);

        //stateMachineOwnerClass.motionTrail.isMotionTrail = false;
        //agent.ResetPath();
    }

    
}
