using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SwordIdle<T> : BossState<T> where T : BossBase<T>
{
    private Animator animator;

    private NavMeshAgent agent;

    private int hashMove = Animator.StringToHash("Move");
    public override void Enter()
    {
        
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
    }

}
