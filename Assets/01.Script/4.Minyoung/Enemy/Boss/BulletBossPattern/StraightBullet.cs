using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBullet : BossState<BulletBoss>
{
    private BulletBoss bulletBoss;
    public override void Enter()
    {
        //���� �ִϸ��̼� ����
        StarightAtk();
    }

    public override void Execute()
    {

    }
        
    IEnumerator StarightAtk()
    {
        bulletBoss?.LookTarget(); //������ �÷��̾ �ٶ󺻴�
        GameObject newBullet = bulletBoss.bullet; //�ҷ� ���� �ٵ� �ν���Ƽ����Ʈ�� �ȵ�
        for (int i = 0; i < 4; i++)
        {
            stateMachineOwnerClass.InstantiateObj(bulletBoss.bullet, stateMachineOwnerClass.transform, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
        //��ŸƮ�ڷ�ƾ ���ڸ��� �ϰ�
        // �ҷ� ���� so���� ��ũ��Ʈ�ٿ��� update���� foward���� �̵�
        yield return new WaitForSeconds(1f);
        //�ҷ��� �����Ѵ�
    }
    public override void Exit()
    {

    }
}
