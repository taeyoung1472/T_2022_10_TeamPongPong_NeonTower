using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motar : BossState<BulletBoss>
{
    public override void Enter()
    {
        Debug.Log("박격포 공격 시작");

    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
    }

}
