using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateExplosion<T> : State<T> where T : EnemyBase<T>
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
        animator?.SetTrigger(hashAttack);

    }
    public override void Execute()
    {

    }
    public override void Exit()
    {

    }
}
