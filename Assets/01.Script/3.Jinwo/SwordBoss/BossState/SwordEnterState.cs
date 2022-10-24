using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SwordEnterState<T> : BossState<T> where T : Sword
{
    private Animator animator;

    private NavMeshAgent agent;

    private Material mat;

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
        stateMachineOwnerClass.Col.enabled = false;
        animator.SetTrigger(hashStartAnime);
        yield return new WaitForSeconds(0.1f);
        while (true)
        {
            if(matValue <=0f)
            {
                break;
            }
            matValue -= 0.005f;
            mat.SetFloat("_Singularity", matValue);
            yield return new WaitForSeconds(0.02f);
        }
        stateMachineOwnerClass.motionTrail.isMotionTrail = true;
        
        yield return new WaitForSeconds(2f);
        stateMachineOwnerClass.Col.enabled = true;
        stateMachine.ChangeState<SwordIdle<T>>();
    }
    public override void Enter()
    {
        AudioManager.PlayAudioRandPitch(stateMachineOwnerClass.startBossClip);
        stateMachineOwnerClass.StartCoroutine(StartAnimationCoroutine());
    }

    public override void Execute()
    {

    }

    public override void Exit()
    {

    }
}
