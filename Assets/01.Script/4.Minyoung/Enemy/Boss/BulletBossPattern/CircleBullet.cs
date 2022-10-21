using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBullet : BossState<BulletBoss>
{
    private BulletBoss bulletBoss;

    public override void Enter()
    {
        Debug.Log("���� ���ݽ���");
        bulletBoss = stateMachineOwnerClass as BulletBoss;
        bulletBoss.StartCoroutine(CircleAtk());
    }

    public override void Execute()
    {
        bulletBoss.LookTarget(); //������ �÷��̾ �ٶ󺻴�
    }
    IEnumerator CircleAtk()
    {

        for (int i = 0; i < bulletBoss.FireCircleCnt; i++)
        {
            //GameObject newBullet = stateMachineOwnerClass.InstantiateObj
            //    (bulletBoss.bullet, stateMachineOwnerClass.transform.position, Quaternion.identity);
            GameObject newBullet = PoolManager.Instance.Pop(PoolType.BulletBossCommonBullet).gameObject;
            newBullet.transform.SetPositionAndRotation(stateMachineOwnerClass.transform.position, Quaternion.identity);

            Rigidbody rid = newBullet.GetComponent<Rigidbody>();

            newBullet.transform.rotation = Quaternion.AngleAxis(i * (360 / bulletBoss.FireCircleCnt), Vector3.up);
            rid.AddForce(newBullet.transform.forward * bulletBoss.CirclePower, ForceMode.Impulse);
        }

        yield return new WaitForSeconds(bulletBoss.StateToIdleTime);

        stateMachine.ChangeState<BulletBossIdle>();
    }
    public override void Exit()
    {
    }
}
