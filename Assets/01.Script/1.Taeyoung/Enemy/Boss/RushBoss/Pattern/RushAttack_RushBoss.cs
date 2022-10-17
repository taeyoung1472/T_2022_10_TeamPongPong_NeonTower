using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RushAttack_RushBoss<T> : BossState<RushBoss> where T : BossBase<T>
{
    public override void Enter()
    {
        stateMachineOwnerClass.ModelReset();
        stateMachineOwnerClass.Agent.ResetPath();
        BossUIManager.Instance.BossPopupText("조심하세요! 보스가 돌진합니다!", 1.5f, 1);
        stateMachineOwnerClass.StartCoroutine(WaitEndRush());
    }

    private IEnumerator WaitEndRush()
    {
        yield return new WaitForSeconds(1f);
        stateMachineOwnerClass.Animator.SetBool("Run", true);
        stateMachineOwnerClass.Agent.speed = stateMachineOwnerClass.AttackDataSO.rushSpeed;
        stateMachineOwnerClass.Agent.SetDestination(stateMachineOwnerClass.Target.position);
    }

    public override void Execute()
    {
        stateMachineOwnerClass.TargetLook();
        if(stateMachineOwnerClass.Agent.remainingDistance <= 0f)
        {
            stateMachine.ChangeState<Idle_RushBoss<RushBoss>>();
        }
    }

    public override void Exit()
    {
        stateMachineOwnerClass.ModelReset();
        stateMachineOwnerClass.Agent.ResetPath();
        stateMachineOwnerClass.Animator.SetBool("Run", false);
        stateMachineOwnerClass.Agent.speed = stateMachineOwnerClass.AttackDataSO.normalSpeed;
    }
}
