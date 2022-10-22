using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class StartWaitState : BossState<BulletBoss>
{
    public override void Enter()
    {
        stateMachineOwnerClass.StartCoroutine(WaitStartAnimation());
    }
    IEnumerator WaitStartAnimation()
    {
        Debug.Log("기다려 멍멍아");
        stateMachineOwnerClass.transform.DOMoveY(0, 1f);
        CameraManager.Instance.CameraShake(20f, 20f, 0.5f);
        Debug.Log("기다려 시작해");
        yield return new WaitForSeconds(5f);
        stateMachine.ChangeState<BulletBossIdle>();
    }
    public override void Execute()
    {
    }

    public override void Exit()
    {
    }

}
