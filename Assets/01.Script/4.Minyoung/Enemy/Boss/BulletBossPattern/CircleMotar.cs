using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CircleMotar : BossState<BulletBoss>
{
    private BulletBoss bulletBoss;

    public override void Enter()
    {
        Debug.Log("박격포 원 공격 시작");
        bulletBoss = stateMachineOwnerClass as BulletBoss;
        stateMachineOwnerClass.StartCoroutine(CircleMotarAtk());
    }
    IEnumerator CircleMotarAtk()
    {
        List<Vector3> circlePostions = new List<Vector3>();

        Vector3 normalVec = Vector3.zero;
        for (int i = 0; i < bulletBoss.FirstMotarCnt; i++)
        {
            normalVec = Quaternion.AngleAxis(i * (360 / bulletBoss.FirstMotarCnt), Vector3.up) * Vector3.forward;
            Vector3 position = stateMachineOwnerClass.transform.position + normalVec * 5f;
            GameObject obj = DangerZone.DrawCircle(position, 4f, 1.5f);
            //GameObject obj = DangerZone.DrawCircle(position, 0.5f, 1.5f);

            circlePostions.Add(position);

            obj.transform.DOScale(new Vector3(5f, 5f, 5f), 1f);

        }

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < bulletBoss.FirstMotarCnt; i++)
        {
            GameObject motarBullet = PoolManager.Instance.Pop(PoolType.BulletBossMortarBullet).gameObject;
            motarBullet.transform.SetPositionAndRotation(circlePostions[i], Quaternion.identity);
            motarBullet.transform.rotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
            motarBullet.transform.position = new Vector3(motarBullet.transform.position.x, 10f, motarBullet.transform.position.z);

            Rigidbody rid = motarBullet.GetComponent<Rigidbody>();

            rid.AddForce(motarBullet.transform.forward * bulletBoss.MotarDownPower, ForceMode.Impulse);

            motarBullet.transform.DOScale(new Vector3(2f, 2f, 2f), 2f);
        }
        circlePostions.Clear();



        for (int i = 0; i < bulletBoss.SecontMotarCnt; i++)
        {
            normalVec = Quaternion.AngleAxis(i * (360 / bulletBoss.SecontMotarCnt), Vector3.up) * Vector3.forward;
            Vector3 position = stateMachineOwnerClass.transform.position + normalVec * 8f;
            GameObject obj = DangerZone.DrawCircle(position, 4f, 1.5f);

            circlePostions.Add(position);

            obj.transform.DOScale(new Vector3(5f, 5f, 5f), 1f);

        }

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < bulletBoss.SecontMotarCnt; i++)
        {
            GameObject motarBullet = PoolManager.Instance.Pop(PoolType.BulletBossMortarBullet).gameObject;
            motarBullet.transform.SetPositionAndRotation(circlePostions[i], Quaternion.identity);
            motarBullet.transform.rotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
            motarBullet.transform.position = new Vector3(motarBullet.transform.position.x, 10f, motarBullet.transform.position.z);

            Rigidbody rid = motarBullet.GetComponent<Rigidbody>();

            rid.AddForce(motarBullet.transform.forward * bulletBoss.MotarDownPower, ForceMode.Impulse);

            motarBullet.transform.DOScale(new Vector3(2f, 2f, 2f), 2f);
        }
        circlePostions.Clear();


        for (int i = 0; i < bulletBoss.ThirdMotarCnt; i++)
        {
            normalVec = Quaternion.AngleAxis(i * (360 / bulletBoss.ThirdMotarCnt), Vector3.up) * Vector3.forward;
            Vector3 position = stateMachineOwnerClass.transform.position + normalVec * 12f;
            GameObject obj = DangerZone.DrawCircle(position, 4f, 1.5f);
            circlePostions.Add(position);

            obj.transform.DOScale(new Vector3(5f, 5f, 5f), 1f);
        }

        yield return new WaitForSeconds(1f);


        for (int i = 0; i < bulletBoss.ThirdMotarCnt; i++)
        {
            GameObject motarBullet = PoolManager.Instance.Pop(PoolType.BulletBossMortarBullet).gameObject;
            motarBullet.transform.SetPositionAndRotation(circlePostions[i], Quaternion.identity);
            motarBullet.transform.rotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
            motarBullet.transform.position = new Vector3(motarBullet.transform.position.x, 10f, motarBullet.transform.position.z);

            Rigidbody rid = motarBullet.GetComponent<Rigidbody>();

            rid.AddForce(motarBullet.transform.forward * bulletBoss.MotarDownPower, ForceMode.Impulse);

            motarBullet.transform.DOScale(new Vector3(2f, 2f, 2f), 2f);
        }
        circlePostions.Clear();

        yield return new WaitForSeconds(bulletBoss.StateToIdleTime);


        stateMachine.ChangeState<BulletBossIdle>();


    }

    public override void Execute()
    {
        bulletBoss.LookTarget();
    }

    public override void Exit()
    {
    }
}
