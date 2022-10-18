using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletBossIdle : BossState<BulletBoss>
{
    
    public override void Enter()
    {
        //���̵� �ִϸ��̼� ����
    }

    public override void Execute()
    {
        if (stateMachine.GetStateDurationTime > 3f)
        {
            stateMachine.ChangeState<StraightMotar>();
            //stateMachine.ChangeState<PlayerMotar>();
            //stateMachine.ChangeState<CircleMotar>();
        }
    }

    public override void Exit()
    {
    }

 
}
