using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
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
        List<Vector3> randomPositions = new List<Vector3>();

        for (int i = 0; i < 5; i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(-6f, 6f), 0f, Random.Range(-6f, 6f));
            Vector3 dir = stateMachineOwnerClass.Target.position + randomPos;

            randomPositions.Add(dir);
            GameObject obj = DangerZone.DrawCircle(dir, 0.5f, 1.5f);
            obj.transform.DOScale(new Vector3(1f, 1f, 1f), 1f);
            yield return new WaitForSeconds(0.3f);
        }

        yield return new WaitForSeconds(bulletBoss.WaringBoomTime);

        for (int i = 0; i < 5; i++)
        {
            GameObject motarBullet = PoolManager.Instance.Pop(PoolType.BulletBossMortarBullet).gameObject;
            motarBullet.transform.SetPositionAndRotation(randomPositions[i], Quaternion.identity);
            motarBullet.transform.rotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
            motarBullet.transform.position = new Vector3(motarBullet.transform.position.x, 10f, motarBullet.transform.position.z);

            Rigidbody rid = motarBullet.GetComponent<Rigidbody>();

            rid.AddForce(motarBullet.transform.forward * bulletBoss.MotarDownPower, ForceMode.Impulse);

            motarBullet.transform.DOScale(new Vector3(2f, 2f, 2f), 2f);

            yield return new WaitForSeconds(0.1f);
        }
        randomPositions.Clear();

        yield return new WaitForSeconds(bulletBoss.StateToIdleTime);


        stateMachine.ChangeState<BulletBossIdle>();

    }

    public override void Exit()
    {
    }
}
