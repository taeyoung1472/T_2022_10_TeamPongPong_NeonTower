using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotar : BossState<BulletBoss>
{
    private BulletBoss bulletBoss;

    public override void Enter()
    {
        Debug.Log("박격포 플레이어 랜덤 공격 시작");
        bulletBoss = stateMachineOwnerClass as BulletBoss;
        stateMachineOwnerClass.StartCoroutine(RandomMotarAtk());
    }

    public override void Execute()
    {
        bulletBoss.LookTarget();   

    }
    IEnumerator RandomMotarAtk()
    {
        bulletBoss.LookTarget();
        for (int i = 0; i < 5; i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(-4f, 4f), 0f, Random.Range(-4f, 4f));
            Vector3 dir = stateMachineOwnerClass.Target.position + randomPos;
            GameObject obj = DangerZone.DrawCircle(dir, 1f, 3f);
            obj.transform.DOScale(new Vector3(1.5f, 1f, 1.5f), 1f);
            obj.transform.DOMoveY(1f, 1f);
            yield return new WaitForSeconds(0.05f);
           GameObject bullet = stateMachineOwnerClass.InstantiateObj(bulletBoss.backBullet, dir, Quaternion.identity);
            bullet.transform.position = new Vector3(bullet.transform.position.x, 3f, bullet.transform.position.z);
        }
       // stateMachine.ChangeState<StraightMotar>();

    }

    public override void Exit()
    {
    }
}
