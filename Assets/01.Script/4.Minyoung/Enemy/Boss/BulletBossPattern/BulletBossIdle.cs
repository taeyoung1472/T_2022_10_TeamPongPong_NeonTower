using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletBossIdle : BossState<BulletBoss>
{
    
    public override void Enter()
    {
        //아이들 애니메이션 실행
    }

    public override void Execute()
    {
        if (stateMachine.GetStateDurationTime > 3f)
        {
            stateMachine.ChangeState<PlayerMotar>();
        }
    }

    public override void Exit()
    {
    }

 
}
