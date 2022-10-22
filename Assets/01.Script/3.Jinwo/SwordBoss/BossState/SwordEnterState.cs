using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SwordEnterState<T> : BossState<T> where T : Sword
{
    private Animator animator;

    private NavMeshAgent agent;

    private Material mat;

    private int hashStartPos = Animator.StringToHash("StartPos");
    private int hashStartAnime = Animator.StringToHash("StartAnime");

    private float matValue = 0;
    public override void OnAwake()
    {
        mat = stateMachineOwnerClass.myMat;
        
        animator = stateMachineOwnerClass.Animator;
        agent = stateMachineOwnerClass.Agent;

        agent.isStopped = true;
    }
    private IEnumerator StartAnimationCoroutine()
    {
        matValue = 1f;
        animator.SetTrigger(hashStartPos);

        stateMachineOwnerClass.Col.enabled = false;
        yield return new WaitForSeconds(0.7f);
        while (true)
        {
            if(matValue <=0f)
            {
                break;
            }
            matValue -= 0.01f;
            mat.SetFloat("_Singularity", matValue);
            yield return null;
        }
        stateMachineOwnerClass.motionTrail.isMotionTrail = true;
        animator.SetTrigger(hashStartAnime);
        yield return new WaitForSeconds(4f);
        stateMachineOwnerClass.Col.enabled = true;
        stateMachine.ChangeState<SwordIdle<T>>();
    }
    public override void Enter()
    {
        stateMachineOwnerClass.StartCoroutine(StartAnimationCoroutine());
    }

    public override void Execute()
    {

    }

    public override void Exit()
    {

    }
}
