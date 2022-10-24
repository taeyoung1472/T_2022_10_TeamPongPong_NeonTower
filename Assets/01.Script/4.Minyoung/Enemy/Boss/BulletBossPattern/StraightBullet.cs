using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBullet : BossState<BulletBoss>
{
    private BulletBoss bulletBoss;
    public override void Enter()
    {
        Debug.Log("직성공격시작");
        bulletBoss = stateMachineOwnerClass as BulletBoss;
        stateMachineOwnerClass.StartCoroutine(StarightAtk());
        //공격 애니메이션 실행
    }

    public override void Execute()
    {
        bulletBoss.LookTarget(); //보스가 플레이어를 바라본다
    }

    IEnumerator StarightAtk()
    {
        Debug.Log("일자어택함");
        for (int i = 0; i < bulletBoss.CycleCnt; i++)
        {
            for (int j = 0; j < bulletBoss.FireCnt; j++)
            {
                GameObject newbullet = PoolManager.Instance.Pop(PoolType.BulletBossCommonBullet).gameObject;
                newbullet.transform.SetPositionAndRotation(stateMachineOwnerClass.transform.position, Quaternion.identity);

                //GameObject newbullet = stateMachineOwnerClass.InstantiateObj
                //    (bulletBoss.bullet, stateMachineOwnerClass.transform.position, Quaternion.identity);

                newbullet.transform.rotation = Quaternion.LookRotation
                    (stateMachineOwnerClass.Target.position - stateMachineOwnerClass.transform.position);

                Rigidbody rigid = newbullet.GetComponent<Rigidbody>();
                rigid.AddForce(newbullet.transform.forward * bulletBoss.StraightPower, ForceMode.Impulse);

                AudioManager.PlayAudioRandPitch(stateMachineOwnerClass.SmallFireClip);

                yield return new WaitForSeconds(bulletBoss.Interval);
            }
            yield return new WaitForSeconds(bulletBoss.CycleInterval);
        }
        yield return new WaitForSeconds(bulletBoss.StateToIdleTime);


        stateMachine.ChangeState<BulletBossIdle>();
    }
    public override void Exit()
    {

    }
}
