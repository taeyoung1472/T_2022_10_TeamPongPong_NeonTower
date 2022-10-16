using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerSkillSummon : BossState<SummonerBoss>
{
    public override void Enter()
    {
        BossUIManager.Instance.BossPopupText("�����ʸ� ��ȯ�մϴ�. �����ʸ� ã�� �μ�����", 1f);
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
