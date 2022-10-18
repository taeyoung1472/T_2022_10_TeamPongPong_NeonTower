using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaveAttack_RushBoss<T> : BossState<RushBoss> where T : BossBase<T>
{
    public override void Enter()
    {
        stateMachineOwnerClass.transform.LookAt(stateMachineOwnerClass.Target.position);
        stateMachineOwnerClass.StartCoroutine(WaveCoroutine());
    }

    private IEnumerator WaveCoroutine()
    {
        float size = 4f;
        Vector3 plus = stateMachineOwnerClass.transform.forward;

        DangerZone.DrawCircle(stateMachineOwnerClass.transform.position + plus * 2f, 4f, 1f);
        yield return new WaitForSeconds(0.1f);
        DangerZone.DrawCircle(stateMachineOwnerClass.transform.position + plus * 2f + plus * size, 4f, 1f);
        yield return new WaitForSeconds(0.1f);
        size += 4f;
        DangerZone.DrawCircle(stateMachineOwnerClass.transform.position + plus * 2f + plus * size, 4f, 1f);
        yield return new WaitForSeconds(0.1f);
        size += 4f;
        DangerZone.DrawCircle(stateMachineOwnerClass.transform.position + plus * 2f + plus * size, 4f, 1f);
        yield return new WaitForSeconds(0.3f);

        stateMachineOwnerClass.Animator.Play("GroundPound");
        stateMachineOwnerClass.Animator.Update(0);
        yield return new WaitUntil(() => stateMachineOwnerClass.Animator.GetCurrentAnimatorStateInfo(0).IsName("GroundPound") == false);
        CameraManager.Instance.CameraShake(12f, 30f, 0.23f);

        yield return new WaitForSeconds(1f);
        stateMachine.ChangeState<Idle_RushBoss<RushBoss>>();
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
    }
}
