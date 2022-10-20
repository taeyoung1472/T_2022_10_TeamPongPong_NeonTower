using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateDashAttack<T> : State<T> where T : EnemyBase<T>
{
    private Animator animator;
    private Transform characterTransform;

    private NavMeshAgent agent;
    Transform target;
    public override void OnAwake()
    {
        agent = stateMachineOwnerClass.GetComponent<NavMeshAgent>();
        animator = stateMachineOwnerClass.GetComponent<Animator>();
        characterTransform = stateMachineOwnerClass.GetComponent<Transform>();

    }
    public override void Enter()
    {
        target = stateMachineOwnerClass.Target.transform;

        
        CoroutineHelper.StartCoroutine(Dash());
    }
    public override void Execute()
    {
        agent.SetDestination(target.position);
    }
    public override void Exit()
    {
        agent.speed = stateMachineOwnerClass.EnemyData.speed;
    }
    public IEnumerator Dash()
    {
        Debug.Log("대쉬 공격 시작");

        agent.isStopped = true;
        //agent.stoppingDistance = 1;
        yield return new WaitForSeconds(0.5f);

        stateMachineOwnerClass.StartMotionTrail();
        agent.isStopped = false;
        float startTime = Time.time;

        while (Time.time < startTime + stateMachineOwnerClass.EnemyData.dashTime)
        {
            agent.speed = stateMachineOwnerClass.EnemyData.dashSpeed;
            yield return null;
        }


        agent.speed = stateMachineOwnerClass.EnemyData.speed;

        stateMachineOwnerClass.StopMotionTrail();
        if (Vector3.Distance(target.position, agent.transform.position) <=
               stateMachineOwnerClass.EnemyData.attackDistance)
        {

            stateMachine.ChangeState<StateMeleeAttack<T>>();
        }
        else
        {
            stateMachine.ChangeState<StateMove<T>>();
        }
        //DashAttack();
    }
}
