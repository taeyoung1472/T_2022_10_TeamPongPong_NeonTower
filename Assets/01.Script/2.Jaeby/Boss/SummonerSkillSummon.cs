using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerSkillSummon : BossState<SummonerBoss>
{
    public override void Enter()
    {
        BossUIManager.Instance.BossPopupText("스포너를 소환합니다. 스포너를 찾아 부수세요", 1f);
        stateMachineOwnerClass.SummonCooltime = 0f;
        stateMachine.ChangeState<SummonerIdle>();
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
    }
}
