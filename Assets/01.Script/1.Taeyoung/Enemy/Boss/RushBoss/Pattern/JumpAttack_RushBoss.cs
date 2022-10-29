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
        originPosY = stateMachineOwnerClass.Model.transform.localPosition.y;
        stateMachineOwnerClass.Model.transform.DOLocalMoveY(20f, 0.5f);
        yield return new WaitForSeconds(0.5f + stateMachineOwnerClass.AttackDataSO.jumpidleTime);
        stateMachineOwnerClass.Agent.enabled = false;

        Vector3 targetPosition = Define.Instance.playerController.transform.position;

        stateMachineOwnerClass.transform.position = targetPosition;
        DangerZone.DrawCircle(targetPosition, 12f, stateMachineOwnerClass.AttackDataSO.fallTime);
        stateMachineOwnerClass.Model.transform.DOLocalMoveY(originPosY, stateMachineOwnerClass.AttackDataSO.fallTime);
        stateMachineOwnerClass.Animator.SetBool("Fall", true);
        yield return new WaitForSeconds(stateMachineOwnerClass.AttackDataSO.fallTime);
        stateMachineOwnerClass.Agent.enabled = true;

        stateMachineOwnerClass.Animator.SetBool("Fall", false);
        stateMachineOwnerClass.Col.enabled = true;
        AudioManager.PlayAudio(stateMachineOwnerClass.ExploClip);
        GameObject obj = stateMachineOwnerClass.ExplosionEffect(targetPosition);
        obj.transform.localScale = Vector3.one * 2f;
        List<Collider> cols = EnemyAttackCollisionCheck.CheckSphere(stateMachineOwnerClass.transform.position, 6f, 1 << 8);
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
