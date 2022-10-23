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
        Debug.Log("��ٷ� �۸۾�");
        stateMachineOwnerClass.transform.DOMoveY(0, 1f);
        CameraManager.Instance.CameraShake(20f, 20f, 0.5f);
        Debug.Log("��ٷ� ������");
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
