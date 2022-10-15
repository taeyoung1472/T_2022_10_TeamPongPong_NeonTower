using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateCannonAttack<T> : State<T> where T : EnemyBase<T>
{
    private Animator animator;
    private Transform characterTransform;

    private NavMeshAgent agent;

    private int hashAttack = Animator.StringToHash("Attack");

    private Transform target;

    public float timer = 0;

    public float turnSmoothTime = 0.1f;
    protected float turnSmoothVelocity;
    public override void OnAwake()
    {
        animator = stateMachineOwnerClass.GetComponent<Animator>();
        characterTransform = stateMachineOwnerClass.GetComponent<Transform>();

        agent = stateMachineOwnerClass.GetComponent<NavMeshAgent>();

    }
    public override void Enter()
    {
        timer = stateMachineOwnerClass.EnemyData.attackDelay;
        target = stateMachineOwnerClass.Target.transform;
        agent.isStopped = true;
        agent.updatePosition = false;
    }
    public override void Execute()
    {

        timer += Time.deltaTime;
        //agent.SetDestination(target.position);

        if (timer > stateMachineOwnerClass.EnemyData.attackDelay)
        {
            //´ëÆ÷ ½î±â
            animator?.SetTrigger(hashAttack);
            stateMachineOwnerClass.isAttack = true;

            timer = 0;
        }

        if (Vector3.Distance(target.position, agent.transform.position) >
               stateMachineOwnerClass.EnemyData.attackDistance)
        {
            stateMachine.ChangeState<StateMove<T>>();
        }
    }
    public override void Exit()
    {
        agent.isStopped = false;
        agent.updatePosition = true;
    }

}
