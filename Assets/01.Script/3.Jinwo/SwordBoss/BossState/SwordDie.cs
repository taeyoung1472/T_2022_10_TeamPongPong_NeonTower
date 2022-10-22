using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SwordDie<T> : BossState<T> where T : Sword
{
    private Animator animator;

    private NavMeshAgent agent;
    private Material mat;

    private float matValue = 0;

    private int hashDie = Animator.StringToHash("Die");
    public override void OnAwake()
    {
        mat = stateMachineOwnerClass.myMat;

        animator = stateMachineOwnerClass.Animator;
        agent = stateMachineOwnerClass.Agent;
    }
    private IEnumerator StartDieAnime()
    {
        
        yield return new WaitForSeconds(0.7f);
        while (true)
        {
            if (matValue >= 1f)
            {
                break;
            }
            matValue += 0.01f;
            mat.SetFloat("_Singularity", matValue);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        //stateMachineOwnerClass.Die();
    }
    public override void Enter()
    {
        stateMachineOwnerClass.motionTrail.isMotionTrail = false;

        animator.SetTrigger(hashDie);
        matValue = 0;
        mat.SetFloat("_Singularity", matValue);
        agent.enabled = false;
        stateMachineOwnerClass.Col.enabled = false;

        stateMachineOwnerClass.StartCoroutine(StartDieAnime());
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
    }

    
}
