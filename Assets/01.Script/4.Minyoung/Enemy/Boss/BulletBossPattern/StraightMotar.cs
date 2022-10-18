using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class StraightMotar : BossState<BulletBoss>
{
    private BulletBoss bulletBoss;
    public override void Enter()
    {
        Debug.Log("박격포 직선 공격 시작");
        bulletBoss = stateMachineOwnerClass as BulletBoss;
        stateMachineOwnerClass.StartCoroutine(StraightMotarAtk());
    }

    public override void Execute()
    {
        bulletBoss.LookTarget();

        #region 옛날에 개고생한 흔적이라 지우기 아까움
        
        {

            //float playerSpd = stateMachineOwnerClass.Target.GetComponent<PlayerController>().CurSpeed;
            //Quaternion playerDir = stateMachineOwnerClass.Target.rotation;
            //float distance = playerSpd * 3f; //3f는 박격포가 떨어질때까찌 걸리는시간
            //Vector3 predictedDir = 
            //    playerDir * (Vector3.right * distance)+ stateMachineOwnerClass.Target.position; //쿼터니언 * 벡터 순서

            //GameObject obj = DangerZone.DrawCircle(predictedDir, 1f, 0.1f);

        }
        #endregion

    }
    IEnumerator StraightMotarAtk()
    {
        List<Vector3> circlePositions = new List<Vector3>();

        Vector3 dir = (stateMachineOwnerClass.Target.position - stateMachineOwnerClass.transform.position).normalized;

        for (int i = 0; i < 5; i++)
        {

            //Vector3 dir = stateMachineOwnerClass.transform.position + Quaternion.LookRotation(stateMachineOwnerClass.Target.position,
            //     stateMachineOwnerClass.transform.position) * new Vector3(0f, 0f, i * 6f);
            Vector3 pos = stateMachineOwnerClass.transform.position + dir * (i * 6);

            GameObject obj = DangerZone.DrawCircle(pos, 0.5f, 2f);

            obj.transform.rotation = Quaternion.LookRotation
                     (stateMachineOwnerClass.Target.position - stateMachineOwnerClass.transform.position);

            obj.transform.DOScale(new Vector3(1f, 1f, 1f), 1f);

            circlePositions.Add(pos);

            yield return new WaitForSeconds(0.3f);
        
        }

        yield return new WaitForSeconds(bulletBoss.WaringBoomTime);

        for (int i = 0; i < 5; i++)
        {
            GameObject motarBullet = PoolManager.Instance.Pop(PoolType.BulletBossMortarBullet).gameObject;
            motarBullet.transform.SetPositionAndRotation(circlePositions[i], Quaternion.identity);
            motarBullet.transform.rotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
            motarBullet.transform.position = new Vector3(motarBullet.transform.position.x, 10f, motarBullet.transform.position.z);

            Rigidbody rid = motarBullet.GetComponent<Rigidbody>();

            rid.AddForce(motarBullet.transform.forward * bulletBoss.MotarDownPower, ForceMode.Impulse);

            motarBullet.transform.DOScale(new Vector3(2f, 2f, 2f), 2f);

            yield return new WaitForSeconds(0.5f);
        }
        circlePositions.Clear();

    }
    public override void Exit()
    {
    }

}
