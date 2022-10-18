using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RushAttack_RushBoss<T> : BossState<RushBoss> where T : BossBase<T>
{
    public override void Enter()
    {
        BossUIManager.Instance.BossPopupText("�����ϼ���! ������ �����մϴ�!", 1.5f, 1);
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
        stateMachineOwnerClass.ModelReset();
        stateMachineOwnerClass.Agent.ResetPath();
        stateMachineOwnerClass.Animator.SetBool("Run", false);
        stateMachineOwnerClass.Agent.speed = stateMachineOwnerClass.AttackDataSO.normalSpeed;
    }
}
