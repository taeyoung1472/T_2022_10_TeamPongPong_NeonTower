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
        //float playerSpd = stateMachineOwnerClass.Target.GetComponent<PlayerController>().CurSpeed;
        //Quaternion playerDir = stateMachineOwnerClass.Target.rotation;
        //float distance = playerSpd * 3f; //3f는 박격포가 떨어질때까찌 걸리는시간
        //Vector3 predictedDir = 
        //    playerDir * (Vector3.right * distance)+ stateMachineOwnerClass.Target.position; //쿼터니언 * 벡터 순서

        //GameObject obj = DangerZone.DrawCircle(predictedDir, 1f, 0.1f);


    }
    IEnumerator StraightMotarAtk()
    {
        bulletBoss.LookTarget();
        
        for (int i = 0; i < 5; i++)
        {

            Vector3 dir = Quaternion.LookRotation(stateMachineOwnerClass.Target.position,
                 stateMachineOwnerClass.transform.position) * new Vector3(0f, 0f, i * 8f);
            GameObject obj = DangerZone.DrawCircle(dir, 1f, 3f);



            obj.transform.DOScale(new Vector3(1.5f, 1f, 1.5f), 1f);
            obj.transform.DOMoveY(1f, 1f);
            //박격포총알발사하고
            //이펙트
            yield return new WaitForSeconds(0.5f);
        }
    }
    public override void Exit()
    {
    }

}
