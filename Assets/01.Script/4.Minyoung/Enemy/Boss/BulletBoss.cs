using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BulletBoss : BossBase<BulletBoss>
{
    public GameObject bullet;

    private void Start()
    {
        bossFsm = new BossStateMachine<BulletBoss>(this, new BulletBossIdle());
        bossFsm.AddStateList(new StraightBullet());
    }
    public void InstantiateObj(GameObject obj, Transform trm, Quaternion rot)
    {
        Instantiate(obj, trm.position, rot);
    }
}
