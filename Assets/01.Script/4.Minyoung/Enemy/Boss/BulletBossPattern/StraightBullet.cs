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
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                bulletBoss.LookTarget(); //보스가 플레이어를 바라본다
                GameObject newbullet = stateMachineOwnerClass.InstantiateObj
                    (bulletBoss.bullet, stateMachineOwnerClass.transform.position, Quaternion.identity);

                newbullet.transform.rotation = Quaternion.LookRotation
                    (stateMachineOwnerClass.Target.position - stateMachineOwnerClass.transform.position);

                //newbullet.GetComponent<goFoward>().A();
                Rigidbody rigid = newbullet.GetComponent<Rigidbody>();
                rigid.AddForce(newbullet.transform.forward * 7f, ForceMode.Impulse);

                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(1f);
        }
        //GameObject newBullet = bulletBoss.bullet; //불렛 생성 근데 인스턴티에이트가 안됨

        //스타트코루틴 들어가자마자 하고
        // 불렛 따라 so만들어서 스크립트붙여서 update에서 foward방향 이동
        //불렛을 생성한다
        yield return new WaitForSeconds(1f);
        stateMachine.ChangeState<CircleBullet>();
    }
    public override void Exit()
    {

    }
}
