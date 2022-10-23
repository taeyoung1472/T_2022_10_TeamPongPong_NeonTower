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
        Debug.Log("시작");
        yield return new WaitForSecondsRealtime(0.2f);
        while (true)
        {
            if (matValue >= 1f)
            {
                break;
            }
            matValue += 0.005f;
            mat.SetFloat("_Singularity", matValue);
            yield return new WaitForSecondsRealtime(0.02f);
        }
        Glitch.GlitchManager.Instance.ZeroValue();
        Debug.Log("끝남");
        yield return new WaitForSeconds(1f);
        //stateMachineOwnerClass.Die();
    }
    public override void Enter()
    {
        //animator.SetTrigger(hashDie);
        Debug.Log("값초기화");
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
