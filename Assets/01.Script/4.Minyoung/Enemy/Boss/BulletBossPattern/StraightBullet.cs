using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBullet : BossState<BulletBoss>
{
    private BulletBoss bulletBoss;
    public override void Enter()
    {
        //공격 애니메이션 실행
        StarightAtk();
    }

    public override void Execute()
    {

    }
        
    IEnumerator StarightAtk()
    {
        bulletBoss?.LookTarget(); //보스가 플레이어를 바라본다
        GameObject newBullet = bulletBoss.bullet; //불렛 생성 근데 인스턴티에이트가 안됨
        for (int i = 0; i < 4; i++)
        {
            stateMachineOwnerClass.InstantiateObj(bulletBoss.bullet, stateMachineOwnerClass.transform, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
        //스타트코루틴 들어가자마자 하고
        // 불렛 따라 so만들어서 스크립트붙여서 update에서 foward방향 이동
        yield return new WaitForSeconds(1f);
        //불렛을 생성한다
    }
    public override void Exit()
    {

    }
}
