using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBossDie : BossState<BulletBoss>
{
    public override void Enter()
    {
        Debug.Log("Á×Àº»óÅÂ");
        stateMachineOwnerClass.BulletBossDieEffect();
        stateMachineOwnerClass.Die();
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
    }
}
