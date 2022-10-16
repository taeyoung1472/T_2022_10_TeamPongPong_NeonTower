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
        int count = 5; //36 10  
        Vector3 normalVec = Vector3.zero;
        for (int i = 0; i < count; i++)
        {
            GameObject obj = DangerZone.DrawCircle(stateMachineOwnerClass.transform.position, 1f, 3f);
            normalVec = Quaternion.AngleAxis(i * (360 / count), Vector3.up) * Vector3.forward;
            Debug.Log(normalVec);
            obj.transform.position = normalVec * 5f;
            obj.transform.DOScale(new Vector3(1.5f, 1f, 1.5f), 1f);
            obj.transform.DOMoveY(1f, 1f);
        }
        yield return new WaitForSeconds(3f);

        for (int i = 0; i < 7; i++)
        {
            
            GameObject obj = DangerZone.DrawCircle(stateMachineOwnerClass.transform.position, 0.5f, 3f);
            normalVec = Quaternion.AngleAxis(i * (360 / 7), Vector3.up) * Vector3.forward;
            Debug.Log(normalVec);
            obj.transform.position = normalVec * 10f;
            obj.transform.DOScale(new Vector3(1.5f, 1f, 1.5f), 1f);
            obj.transform.DOMoveY(1f, 1f);
        }

    }
    public override void Execute()
    {
        bulletBoss.LookTarget();
    }

    public override void Exit()
    {
    }
}
