using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBullet : BossState<BulletBoss>
{
    private BulletBoss bulletBoss;

    public override void Enter()
    {
        Debug.Log("원형 공격시작");
        bulletBoss = stateMachineOwnerClass as BulletBoss;
        bulletBoss.StartCoroutine(CircleAtk());
    }

    public override void Execute()
    {
    }
    IEnumerator CircleAtk()
    {
        int count = 15; //36 10  
        for (int i = 0; i < count; i++)
        {
            GameObject newBullet = stateMachineOwnerClass.InstantiateObj
                (bulletBoss.bullet, stateMachineOwnerClass.transform.position, Quaternion.identity);

            Rigidbody rid = newBullet.GetComponent<Rigidbody>();
            //Vector3 dirVec = new Vector3(Mathf.Cos(Mathf.PI * 2 * i) / count, 0f, Mathf.Sin(Mathf.PI * 2 * i) / count);
            //rid.AddForce(dirVec.normalized * 3, ForceMode.Impulse);

            newBullet.transform.rotation = Quaternion.AngleAxis(i * (360 / count), Vector3.up);
            Debug.Log(newBullet.transform.eulerAngles.y);
            rid.AddForce(newBullet.transform.forward * 3f, ForceMode.Impulse);

            //Vector3 rotVec = Vector3.forward * 360 * i / count + Vector3.forward * 90;
            //newBullet.transform.Rotate(rotVec);
        }
        yield return new WaitForSeconds(1f);
        stateMachine.ChangeState<FirecrackerBullet>();
        Debug.Log("ㅁㅁㄴㅇ");
    }
    public override void Exit()
    {
    }
}
