using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerSkillSlow : BossState<SummonerBoss>
{
    public override void Enter()
    {
        stateMachineOwnerClass.ModelReset();
        BossUIManager.Instance.BossPopupText("느려지는 장판을 소환합니다 !", 1.5f, 1);
        stateMachineOwnerClass.SlowCooltime = 0f;
        stateMachineOwnerClass.StartCoroutine(SlowAnimationEndWaiting());
    }

    private IEnumerator SlowAnimationEndWaiting()
    {
        Vector2 randomCircle = Random.insideUnitCircle;
        Vector3 pos = Vector3.zero;
        if (stateMachineOwnerClass.AttackDataSO.immPlayer)
        {
            pos = stateMachineOwnerClass.Target.position;
            pos.y = 0f;
        }
        else
        {
            pos = stateMachineOwnerClass.transform.position + new Vector3(randomCircle.x, 0f, randomCircle.y) * stateMachineOwnerClass.AttackDataSO.slowRandomCircle;
        }

        GameObject obj = stateMachineOwnerClass.MonoInstantiate(
            stateMachineOwnerClass.AttackDataSO.slowClrcle == null ? new GameObject() : stateMachineOwnerClass.AttackDataSO.slowClrcle,
            pos,
            Quaternion.identity
            );
        obj.transform.localScale = new Vector3(stateMachineOwnerClass.AttackDataSO.slowScale, 0.1f, stateMachineOwnerClass.AttackDataSO.slowScale);
        obj.GetComponent<SlowField>().SlowIntensity = stateMachineOwnerClass.AttackDataSO.slowIntensity;
        stateMachineOwnerClass.MonoDestroy(obj, stateMachineOwnerClass.AttackDataSO.slowAttackDuration);

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
