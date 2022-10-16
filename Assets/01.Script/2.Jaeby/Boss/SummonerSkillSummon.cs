using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerSkillSummon : BossState<SummonerBoss>
{
    public override void Enter()
    {
        stateMachineOwnerClass.ModelReset();
        BossUIManager.Instance.BossPopupText("스포너를 소환합니다. 스포너를 찾아 부수세요", 1f, 1);
        stateMachineOwnerClass.SummonCooltime = 0f;
        stateMachineOwnerClass.StartCoroutine(SummonAnimationEndWaiting());
    }

    private IEnumerator SummonAnimationEndWaiting()
    {
        Vector2 randomCircle = Random.insideUnitCircle;
        Vector3 pos = new Vector3(randomCircle.x, 0f, randomCircle.y) * stateMachineOwnerClass.AttackDataSO.spawnerRandomCircle;
        stateMachineOwnerClass.MonoInstantiate(
            stateMachineOwnerClass.AttackDataSO.spawner == null ? new GameObject() : stateMachineOwnerClass.AttackDataSO.spawner,
            pos,
            Quaternion.identity
            );
        stateMachineOwnerClass.Animator.Play("Summon");
        stateMachineOwnerClass.Animator.Update(0);
        yield return new WaitUntil(() => stateMachineOwnerClass.Animator.GetCurrentAnimatorStateInfo(0).IsName("Summon") == false);
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
