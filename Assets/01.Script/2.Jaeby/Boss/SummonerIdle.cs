using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerIdle : BossState<SummonerBoss>
{
    private float _randomIdleTime = 0f;
    private float _patternRange = 5f; // walkLimit보다 가까이 있으면 패턴 수행
    private Vector3 _targetPos = Vector3.zero;

    public override void Enter()
    {
        Debug.Log("Idle State");
        _randomIdleTime = Random.Range(1f, 2f);
    }


    public override void Execute()
    {
        if (stateMachine.GetStateDurationTime > _randomIdleTime)
        {
            if (stateMachineOwnerClass.Target != null)
            {
                _targetPos = stateMachineOwnerClass.Target.position;
                _targetPos.y = stateMachineOwnerClass.transform.position.y;
                if (Vector3.Distance(stateMachineOwnerClass.transform.position, _targetPos) < _patternRange)
                {

                }
                else
                {
                    stateMachine.ChangeState<SummonerWalk>();
                }
            }
        }
    }

    public override void Exit()
    {
    }
}
