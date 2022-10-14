using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Idle<T> : BossState<DemoBoss> where T : BossBase<T>
{
    public override void Enter()
    {
    }

    public override void Execute()
    {
        if (stateMachine.GetStateDurationTime > 5)
        {
            stateMachine.ChangeState<CircleAttack<DemoBoss>>();
        }
    }

    public override void Exit()
    {
    }
}
