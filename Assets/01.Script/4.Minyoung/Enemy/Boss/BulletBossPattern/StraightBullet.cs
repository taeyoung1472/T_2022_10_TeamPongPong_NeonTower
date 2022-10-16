using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBullet : BossState<BulletBoss>
{
    private BulletBoss bulletBoss;
    public override void Enter()
    {
        Debug.Log("�������ݽ���");
        bulletBoss = stateMachineOwnerClass as BulletBoss;
        stateMachineOwnerClass.StartCoroutine(StarightAtk());
        //���� �ִϸ��̼� ����
    }

    public override void Execute()
    {
        bulletBoss.LookTarget(); //������ �÷��̾ �ٶ󺻴�

    }

    IEnumerator StarightAtk()
    {
        Debug.Log("���ھ�����");
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                bulletBoss.LookTarget(); //������ �÷��̾ �ٶ󺻴�
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
        //GameObject newBullet = bulletBoss.bullet; //�ҷ� ���� �ٵ� �ν���Ƽ����Ʈ�� �ȵ�

        //��ŸƮ�ڷ�ƾ ���ڸ��� �ϰ�
        // �ҷ� ���� so���� ��ũ��Ʈ�ٿ��� update���� foward���� �̵�
        //�ҷ��� �����Ѵ�
        yield return new WaitForSeconds(1f);
        stateMachine.ChangeState<CircleBullet>();
    }
    public override void Exit()
    {

    }
}
