using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class JumpAttack_RushBoss<T> : BossState<RushBoss> where T : BossBase<T>
{
    public override void Enter()
    {
        stateMachineOwnerClass.After.isMotionTrail = true;
        stateMachineOwnerClass.After._bakingCycle = 0.05f;
        stateMachineOwnerClass.After._data.duration = 0.2f;
        BossUIManager.Instance.BossPopupText("보스가 점프합니다!", 1.5f, 2);

        stateMachineOwnerClass.ModelReset();
        stateMachineOwnerClass.Agent.ResetPath();
        stateMachineOwnerClass.Agent.isStopped = true;
        stateMachineOwnerClass.Agent.velocity = Vector3.zero;
        stateMachineOwnerClass.StartCoroutine(JumpCoroutine());
    }

    private IEnumerator JumpCoroutine()
    {
        float originPosY = 0f;

        stateMachineOwnerClass.Animator.Play("Jump");
        stateMachineOwnerClass.Animator.Update(0);
        yield return new WaitUntil(() => stateMachineOwnerClass.Animator.GetCurrentAnimatorStateInfo(0).IsName("Jump") == false);
        stateMachineOwnerClass.Col.enabled = false;
        originPosY = stateMachineOwnerClass.Model.transform.position.y;
        stateMachineOwnerClass.Model.transform.DOMoveY(20f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        DangerZone.DrawCircle(stateMachineOwnerClass.transform.position, 12f, stateMachineOwnerClass.AttackDataSO.jumpidleTime + 0.3f);
        yield return new WaitForSeconds(stateMachineOwnerClass.AttackDataSO.jumpidleTime);

        Vector3 playerPos = stateMachineOwnerClass.Target.position;
        stateMachineOwnerClass.transform.position = new Vector3(playerPos.x, 20f, playerPos.z);
        stateMachineOwnerClass.Model.transform.DOMoveY(originPosY, 0.5f);
        stateMachineOwnerClass.Animator.SetBool("Fall", true);
        stateMachineOwnerClass.Animator.Update(0);
        yield return new WaitForSeconds(0.5f);

        stateMachineOwnerClass.Animator.SetBool("Fall", false);
        stateMachineOwnerClass.Col.enabled = true;
        AudioManager.PlayAudio(stateMachineOwnerClass.ExploClip);
        stateMachineOwnerClass.ExplosionEffect(stateMachineOwnerClass.transform.position);
        List<Collider> cols = EnemyAttackCollisionCheck.CheckSphere(stateMachineOwnerClass.transform.position, 12f, 1 << 8);
        EnemyAttackCollisionCheck.ApplyDamaged(cols, 1);

        stateMachine.ChangeState<Idle_RushBoss<RushBoss>>();
    }

    public override void Execute()
    {

    }

    public override void Exit()
    {
        stateMachineOwnerClass.After.isMotionTrail = false;
        stateMachineOwnerClass.After._bakingCycle = 0.1f;
        stateMachineOwnerClass.After._data.duration = 0.1f;
        stateMachineOwnerClass.ModelReset();
        stateMachineOwnerClass.Agent.ResetPath();

        stateMachineOwnerClass.Agent.isStopped = false;
        stateMachineOwnerClass.ModelReset();
    }
}
