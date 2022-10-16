using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerSkillSlow : BossState<SummonerBoss>
{
    public override void Enter()
    {
        BossUIManager.Instance.BossPopupText("느려지는 장판을 소환합니다 !", 1f);
        stateMachineOwnerClass.SlowCooltime = 0f;
        stateMachine.ChangeState<SummonerIdle>();
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
    }

}
