using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class Move_RushBoss<T> : BossState<RushBoss> where T : BossState<T>
{
    Vector3 goalPoint;
    public override void Enter()
    {
        goalPoint = stateMachineOwnerClass.Target.position;
    }

    public override void Execute()
    {
        //if()
    }

    public override void Exit()
    {

    }
}
