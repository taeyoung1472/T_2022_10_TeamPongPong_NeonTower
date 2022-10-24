using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirecrackerBullet : BossState<BulletBoss>
{
    private BulletBoss bulletBoss;
    

    public override void Enter()
    {
        Debug.Log("ÆøÁ× °ø°Ý½ÃÀÛ");
        bulletBoss = stateMachineOwnerClass as BulletBoss;
        AudioManager.PlayAudioRandPitch(stateMachineOwnerClass.BigFireClip);
        bulletBoss.StartCoroutine(BoomAtk());
    }

    public override void Execute()
    {
        bulletBoss.LookTarget();
    }
    IEnumerator BoomAtk()
    {
        List<Transform> bullets = new List<Transform>();

        //GameObject newBullet = stateMachineOwnerClass.InstantiateObj(
        //    bulletBoss.firecrackerBullet, stateMachineOwnerClass.transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);

        GameObject newBullet = PoolManager.Instance.Pop(PoolType.BulletBossFirecrackerBullet).gameObject;
        newBullet.transform.SetPositionAndRotation(stateMachineOwnerClass.transform.position + new Vector3(0f, 1f, 0f) , Quaternion.identity);


        newBullet.transform.DOScale(new Vector3(5f, 5f, 5f), bulletBoss.BigBulletDestoryTime);

        newBullet.transform.rotation = Quaternion.LookRotation
                   (stateMachineOwnerClass.Target.position - stateMachineOwnerClass.transform.position);
     
        Rigidbody rigid = newBullet.GetComponent<Rigidbody>();
        rigid.AddForce(newBullet.transform.forward * bulletBoss.BoomCirclePower, ForceMode.Impulse);

        yield return new WaitForSeconds(bulletBoss.BigBulletDestoryTime);

        stateMachineOwnerClass.DestroyObj(newBullet);

        CameraManager.Instance.CameraShake(15f, 30f, 0.25f);

        GameObject circleBullet = null;

        AudioManager.PlayAudio(stateMachineOwnerClass.BoomClip);
        for (int i = 0; i < bulletBoss.BoomCircleCnt; i++)
        {
            circleBullet = PoolManager.Instance.Pop(PoolType.BulletBossCommonBullet).gameObject;
            circleBullet.transform.SetPositionAndRotation(newBullet.transform.position, Quaternion.identity);

            //circleBullet = stateMachineOwnerClass.InstantiateObj(
            //     bulletBoss.bullet, newBullet.transform.position, Quaternion.identity);

            Rigidbody rid = circleBullet.GetComponent<Rigidbody>();
            circleBullet.transform.rotation = Quaternion.AngleAxis(i * (360 / bulletBoss.BoomCircleCnt), Vector3.up);
            rid.AddForce(circleBullet.transform.forward * bulletBoss.BoomPower, ForceMode.Impulse);
            bullets.Add(circleBullet.transform);
        }
        //stateMachineOwnerClass.DestroyObj(circleBullet);

        yield return new WaitForSeconds(0.3f);
        AudioManager.PlayAudio(stateMachineOwnerClass.BoomClip, 1, 1.25f);

        CameraManager.Instance.CameraShake(20f, 30f, 0.25f);

        foreach (Transform bulletTrm in bullets)
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject copyBullet = stateMachineOwnerClass.InstantiateObj(
                    bulletTrm.gameObject, bulletTrm.position, bulletTrm.rotation);
                SetBulletInfo(copyBullet, copyBullet.transform);
            }
            SetBulletInfo(circleBullet, bulletTrm);
        }
        yield return new WaitForSeconds(bulletBoss.StateToIdleTime);

        stateMachine.ChangeState<BulletBossIdle>();
    }
    private void SetBulletInfo(GameObject circleBullet, Transform bulletTrm)
    {
        float randomAngle, randomRadius;
        randomAngle = Random.Range(0f, 360f);
        randomRadius = Random.Range(0f, 3f);

        Vector3 addDir = Quaternion.Euler(new Vector3(0, randomAngle, 0))*Vector3.up * randomRadius;
        bulletTrm.position += addDir;

        Rigidbody rid = circleBullet.GetComponent<Rigidbody>();
        rid.velocity = Vector3.zero;
        bulletTrm.rotation *= Quaternion.Euler(new Vector3(0, Random.Range(-50f, 50f), 0));
        rid.AddForce(circleBullet.transform.forward * bulletBoss.BoomPower, ForceMode.Impulse);
    }

    public override void Exit()
    {
    }
}
