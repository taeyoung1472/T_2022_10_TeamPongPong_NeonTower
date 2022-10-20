using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RushAttack_RushBoss<T> : BossState<RushBoss> where T : BossBase<T>
{
    public override void Enter()
    {
        stateMachineOwnerClass.After.isMotionTrail = true;
        stateMachineOwnerClass.Agent.speed = stateMachineOwnerClass.AttackDataSO.rushSpeed;
        stateMachineOwnerClass.After._bakingCycle = 0.05f;
        stateMachineOwnerClass.After._data.duration = 0.2f;
        stateMachineOwnerClass.RushForceField.SetActive(true);
        stateMachineOwnerClass.Animator.SetBool("Rush", true);
        BossUIManager.Instance.BossPopupText("조심하세요! 보스가 돌진합니다!", 1.5f, 2);
    }

    public override void Execute()
    {
        stateMachineOwnerClass.TargetLook();
        stateMachineOwnerClass.Agent.SetDestination(stateMachineOwnerClass.Target.position);
        if(stateMachine.GetStateDurationTime > stateMachineOwnerClass.AttackDataSO.rushDuration)
        {
            stateMachine.ChangeState<Idle_RushBoss<RushBoss>>();
        }
    }

    public override void Exit()
    {
        stateMachineOwnerClass.After.isMotionTrail = false;
        stateMachineOwnerClass.After._bakingCycle = 0.1f;
        stateMachineOwnerClass.After._data.duration = 0.1f;
        stateMachineOwnerClass.ModelReset();
        stateMachineOwnerClass.Agent.ResetPath();
        stateMachineOwnerClass.Animator.SetBool("Rush", false);
        stateMachineOwnerClass.RushForceField.SetActive(false);
        stateMachineOwnerClass.Agent.speed = stateMachineOwnerClass.AttackDataSO.normalSpeed;
    }
}
