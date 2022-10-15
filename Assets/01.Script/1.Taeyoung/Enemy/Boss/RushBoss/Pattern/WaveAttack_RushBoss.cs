using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaveAttack_RushBoss<T> : BossState<RushBoss> where T : BossBase<T>
{
    public override void Enter()
    {
        Debug.Log("Circle Attack ½ÇÇà!");
    }

    public override void Execute()
    {
        float angle = 0;
        for (int i = 0; i < 12; i++)
        {
            DangerZone.DrawCircle(stateMachineOwnerClass.transform.position + new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad)) * 10, 2, 3);
            angle += 360 / 12;
        }
        stateMachine.ChangeState<Idle_RushBoss<RushBoss>>();
    }

    public override void Exit()
    {
    }
}
