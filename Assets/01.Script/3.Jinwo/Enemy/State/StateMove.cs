using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateMove<T> : State<T> where T : EnemyBase<T> 
{
    private Animator animator;
    
    private NavMeshAgent agent;

    private int hashMove = Animator.StringToHash("Move");
    Coroutine updatePath;

    // 등록시 초기화
    public override void OnAwake()
    {
        //Debug.Log(typeof(T).Name);

        animator = stateMachineOwnerClass.GetComponent<Animator>();

        agent = stateMachineOwnerClass.GetComponent<NavMeshAgent>();

        agent.stoppingDistance = stateMachineOwnerClass.EnemyData.stoppingDistance;

        agent.speed = stateMachineOwnerClass.EnemyData.speed;
    }

    public override void Enter()
    {   
        animator?.SetBool(hashMove, true);
        //updatePath = CoroutineHelper.StartCoroutine(UpdatePath());

    }
    public override void Execute()
    {
        if (stateMachineOwnerClass.EnemyData.dashDistance != 0) //대쉬 적일떄만
        {
            if (Vector3.Distance(stateMachineOwnerClass.Target.transform.position, agent.transform.position) <=
               stateMachineOwnerClass.EnemyData.dashDistance
               &&
               Time.time > stateMachineOwnerClass.lastAttackTime + stateMachineOwnerClass.EnemyData.attackDelay)
            {
                stateMachineOwnerClass.ChangeAttack();
            }
            else
            {
                agent?.SetDestination(stateMachineOwnerClass.Target.transform.position);
            }
        }
        else
        {
            if (Vector3.Distance(stateMachineOwnerClass.Target.transform.position, agent.transform.position) <=
               stateMachineOwnerClass.EnemyData.attackDistance
               &&
               Time.time > stateMachineOwnerClass.lastAttackTime + stateMachineOwnerClass.EnemyData.attackDelay)
            {
                stateMachineOwnerClass.ChangeAttack();

            }
            else
            {
                agent?.SetDestination(stateMachineOwnerClass.Target.transform.position);
            }
        }
        
        
        
    }
    public IEnumerator UpdatePath()
    {
        while(!stateMachineOwnerClass.Dead)
        {
            agent?.SetDestination(stateMachineOwnerClass.Target.transform.position);
            yield return new WaitForSeconds(0.2f);
        }
    }
    //사망햇을 때 코루틴 멈추기
    public void StopUpdatePath()
    {
        stateMachineOwnerClass.StopCoroutine(updatePath);
    }
    public override void Exit()
    {
        animator?.SetBool(hashMove, false);

        agent.ResetPath();
    }
}
