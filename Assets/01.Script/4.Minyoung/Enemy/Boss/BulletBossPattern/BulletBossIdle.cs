using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletBossIdle : BossState<BulletBoss>
{
    private int randIdx = 0;

    private BulletBoss bulletBoss;
    private int randTime = 0;
    public override void Enter()
    {
        bulletBoss = stateMachineOwnerClass as BulletBoss;
        randIdx = stateMachineOwnerClass.RandomIndex();
        randTime = Random.Range(1, 2);
        //아이들 애니메이션 실행
    }

    public override void Execute()
    {
        bulletBoss.LookTarget();

        if (stateMachine.GetStateDurationTime > randTime)
        {
            switch (randIdx)
            {
                case 0:
                    stateMachine.ChangeState<StraightMotar>();
                    break;
                case 1:
                    stateMachine.ChangeState<CircleMotar>();
                    break;
                case 2:
                    stateMachine.ChangeState<PlayerMotar>();
                    break;
                case 3:
                    stateMachine.ChangeState<StraightBullet>();
                    break;
                case 4:
                    stateMachine.ChangeState<CircleBullet>();
                    break;
                case 5:
                    stateMachine.ChangeState<FirecrackerBullet>();

                    break;
            }
        }
    }

    public override void Exit()
    {
    }


}
