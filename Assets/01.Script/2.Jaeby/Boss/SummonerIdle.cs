using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerIdle : BossState<SummonerBoss>
{
    private float _randomIdleTime = 0f;

    public override void Enter()
    {
        Debug.Log("Idle State");
        _randomIdleTime = Random.Range(1f, 2f);
    }


    public override void Execute()
    {
        if (stateMachine.GetStateDurationTime > _randomIdleTime)
        {
            stateMachine.ChangeState<SummonerWalk>();
        }
    }

    public override void Exit()
    {
    }
}
