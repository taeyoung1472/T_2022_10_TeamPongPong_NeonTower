using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Idle_RushBoss<T> : BossState<RushBoss> where T : BossBase<T>
{
    public override void Enter()
    {
    }

    public override void Execute()
    {
        if (stateMachine.GetStateDurationTime > 5)
        {
            stateMachine.ChangeState<RushAttack_RushBoss<RushBoss>>();
        }
    }

    public override void Exit()
    {
    }
}
