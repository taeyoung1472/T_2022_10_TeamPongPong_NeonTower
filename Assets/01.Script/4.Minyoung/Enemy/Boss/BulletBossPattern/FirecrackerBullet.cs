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
        bulletBoss.StartCoroutine(BoomAtk());
    }

    public override void Execute()
    {
    }
    IEnumerator BoomAtk()
    {
        List<Transform> bullets = new List<Transform>();

        GameObject newBullet = stateMachineOwnerClass.InstantiateObj(
            bulletBoss.firecrackerBullet, stateMachineOwnerClass.transform.position, Quaternion.identity);
        //ÅÍÁö´Â ÆøÁ× ÆÄÆ¼Å¬
        //»õ·Î¿î ÃÑ¾Ë
        yield return new WaitForSeconds(1f);
        stateMachineOwnerClass.DestroyObj(newBullet);
        int count = 10;
        GameObject circleBullet = null;
        for (int i = 0; i < count; i++)
        {
             circleBullet = stateMachineOwnerClass.InstantiateObj(
                 bulletBoss.bullet, stateMachineOwnerClass.transform.position, Quaternion.identity);

            Rigidbody rid = circleBullet.GetComponent<Rigidbody>();
            circleBullet.transform.rotation = Quaternion.AngleAxis(i * (360 / count), Vector3.up);
            rid.AddForce(circleBullet.transform.forward * 3f, ForceMode.Impulse);
            bullets.Add(circleBullet.transform);
        }

        yield return new WaitForSeconds(1f);
        
        Debug.Log("¿øÁö¿öÁü");
        foreach (Transform bulletTrm in bullets)
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject copyBullet = stateMachineOwnerClass.InstantiateObj(
                    bulletTrm.gameObject, bulletTrm.position, bulletTrm.rotation);
                SetBulletInfo(copyBullet, copyBullet.transform);
            }
            SetBulletInfo(circleBullet, bulletTrm);
        }
        yield return new WaitForSeconds(1f);
        stateMachine.ChangeState<PlayerMotar>();
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
        rid.AddForce(circleBullet.transform.forward * 7f, ForceMode.Impulse);
    }

    public override void Exit()
    {
    }
}
