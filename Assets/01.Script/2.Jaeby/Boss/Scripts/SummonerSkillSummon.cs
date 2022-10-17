using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerSkillSummon : BossState<SummonerBoss>
{
    public override void Enter()
    {
        stateMachineOwnerClass.ModelReset();
        BossUIManager.Instance.BossPopupText("보스가 적을 소환합니다.", 1.5f, 1);
        stateMachineOwnerClass.SummonCooltime = 0f;
        stateMachineOwnerClass.StartCoroutine(SummonAnimationEndWaiting());
    }

    private IEnumerator SummonAnimationEndWaiting()
    {
        stateMachineOwnerClass.StartCoroutine(SpawnEnemys());
        stateMachineOwnerClass.Animator.Play("Summon");
        stateMachineOwnerClass.Animator.Update(0);
        yield return new WaitUntil(() => stateMachineOwnerClass.Animator.GetCurrentAnimatorStateInfo(0).IsName("Summon") == false);
        stateMachine.ChangeState<SummonerIdle>();
    }

    private IEnumerator SpawnEnemys()
    {
        for(int i = 0; i <stateMachineOwnerClass.AttackDataSO.enemySpawnCount; i++)
        {
            yield return new WaitForSeconds(stateMachineOwnerClass.AttackDataSO.enemySpawnDelay);

            Vector2 randomCircle = Random.insideUnitCircle;
            Vector3 pos = new Vector3(randomCircle.x, 0f, randomCircle.y) * stateMachineOwnerClass.AttackDataSO.spawnerRandomCircle;
            GameObject spawner = stateMachineOwnerClass.MonoInstantiate(
                stateMachineOwnerClass.AttackDataSO.enemySpawner,
                stateMachineOwnerClass.transform.position + pos,
                Quaternion.identity
                );

            int randomIdx = Random.Range(0, stateMachineOwnerClass.AttackDataSO.spawnableEnemys.Count);
            PoolType obj = stateMachineOwnerClass.AttackDataSO.spawnableEnemys[randomIdx];
            spawner.GetComponent<SummonersEnemySpawner>().SpawnObj = obj;
            spawner.GetComponent<SummonersEnemySpawner>().Target = stateMachineOwnerClass.Target.gameObject;
        }
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
