using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerSkillSlow : BossState<SummonerBoss>
{
    public override void Enter()
    {
        stateMachineOwnerClass.ModelReset();
        BossUIManager.Instance.BossPopupText("느려지는 장판을 소환합니다 !", 1f, 1);
        stateMachineOwnerClass.SlowCooltime = 0f;
        stateMachineOwnerClass.StartCoroutine(SlowAnimationEndWaiting());
    }

    private IEnumerator SlowAnimationEndWaiting()
    {
        Vector2 randomCircle = Random.insideUnitCircle;
        Vector3 pos = new Vector3(randomCircle.x, 0f, randomCircle.y) * stateMachineOwnerClass.AttackDataSO.slowRandomCircle;
        stateMachineOwnerClass.MonoInstantiate(
            stateMachineOwnerClass.AttackDataSO.slowClrcle == null ? new GameObject() : stateMachineOwnerClass.AttackDataSO.slowClrcle,
            pos,
            Quaternion.identity
            );
        stateMachineOwnerClass.Animator.Play("Slow");
        stateMachineOwnerClass.Animator.Update(0);
        yield return new WaitUntil(() => stateMachineOwnerClass.Animator.GetCurrentAnimatorStateInfo(0).IsName("Slow") == false);
        stateMachine.ChangeState<SummonerIdle>();
    }

    public override void Execute()
    {
        stateMachineOwnerClass.TargetLook();
    }

    public override void Exit()
    {
        stateMachineOwnerClass.ModelReset();
    }

}
