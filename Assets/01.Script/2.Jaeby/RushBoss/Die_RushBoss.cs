using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die_RushBoss<T> : BossState<RushBoss> where T : BossBase<T>
{
    public override void Enter()
    {
        stateMachineOwnerClass.Col.enabled = false;
        stateMachineOwnerClass.Agent.enabled = false;
        stateMachineOwnerClass.StartCoroutine(SetDissolve());
    }

    private IEnumerator SetDissolve()
    {
        float time = 1f;
        while(time >= -2f)
        {
            stateMachineOwnerClass.Mat.SetFloat("_DissolveBeginOffset", time);
            time -= Time.deltaTime * 2.5f;
            yield return null;
        }
        time = -2f;
        stateMachineOwnerClass.Mat.SetFloat("_DissolveBeginOffset", time);
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
    }
}
