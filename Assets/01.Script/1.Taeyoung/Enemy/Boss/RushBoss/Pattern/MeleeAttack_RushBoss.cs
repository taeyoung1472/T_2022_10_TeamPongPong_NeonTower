using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MeleeAttack_RushBoss<T> : BossState<RushBoss> where T : BossBase<T>
{
    public override void Enter()
    {
        stateMachineOwnerClass.ModelReset();
        stateMachineOwnerClass.Agent.ResetPath();
        stateMachineOwnerClass.Agent.isStopped = true;
        stateMachineOwnerClass.Agent.velocity = Vector3.zero;
        stateMachineOwnerClass.StartCoroutine(PunchCoroutine());
    }

    private IEnumerator PunchCoroutine()
    {
        stateMachineOwnerClass.LookTarget();
        DangerZone.DrawBox(stateMachineOwnerClass.transform.position + stateMachineOwnerClass.transform.forward * 3f, stateMachineOwnerClass.transform.rotation, 
            new Vector3(3.5f, 0.1f, 6.5f), stateMachineOwnerClass.AttackDataSO.punchDelays[0]);

        yield return new WaitForSeconds(stateMachineOwnerClass.AttackDataSO.punchDelays[0]);
        stateMachineOwnerClass.Animator.SetTrigger("Punch");
        stateMachineOwnerClass.Animator.Update(0);
        CameraManager.Instance.CameraShake(5f, 20f, 0.2f);


        stateMachineOwnerClass.LookTarget();
        DangerZone.DrawArc(stateMachineOwnerClass.transform.position, stateMachineOwnerClass.transform.forward, 180f, 
            new Vector3(6.5f, 0.1f, 8.5f), stateMachineOwnerClass.AttackDataSO.punchDelays[1]);

        yield return new WaitForSeconds(stateMachineOwnerClass.AttackDataSO.punchDelays[1]);
        stateMachineOwnerClass.Animator.SetTrigger("Punch");
        stateMachineOwnerClass.Animator.Update(0);
        CameraManager.Instance.CameraShake(7f, 20f, 0.3f);


        stateMachineOwnerClass.LookTarget();
        DangerZone.DrawBox(stateMachineOwnerClass.transform.position + stateMachineOwnerClass.transform.forward * 6f, stateMachineOwnerClass.transform.rotation, 
            new Vector3(4f, 0.1f, 12f), stateMachineOwnerClass.AttackDataSO.punchDelays[2]);

        yield return new WaitForSeconds(stateMachineOwnerClass.AttackDataSO.punchDelays[2]);
        stateMachineOwnerClass.Animator.SetTrigger("Punch");
        stateMachineOwnerClass.Animator.Update(0);
        CameraManager.Instance.CameraShake(10f, 30f, 0.23f);
        yield return new WaitUntil(() => stateMachineOwnerClass.Animator.GetCurrentAnimatorStateInfo(0).IsName("Punch2") == false);

        if(stateMachineOwnerClass.GetDistance() > stateMachineOwnerClass.AttackDataSO.rushDistance)
        {
            stateMachine.ChangeState<RushAttack_RushBoss<RushBoss>>();
        }
        else if(stateMachineOwnerClass.GetDistance() < stateMachineOwnerClass.AttackDataSO.groundPoundSize)
        {
            stateMachine.ChangeState<GroundPoundAttack_RushBoss<RushBoss>>();
        }
        else
        {
            stateMachine.ChangeState<WaveAttack_RushBoss<RushBoss>>();
        }
    }

    public override void Execute()
    {

    }

    public override void Exit()
    {
        stateMachineOwnerClass.Agent.isStopped = false;
        stateMachineOwnerClass.ModelReset();
    }
}
