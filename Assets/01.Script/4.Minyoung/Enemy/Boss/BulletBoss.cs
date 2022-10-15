using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BulletBoss : BossBase<BulletBoss>
{
    public GameObject bullet;
    public GameObject firecrackerBullet;
    private void Start()
    {
        bossFsm = new BossStateMachine<BulletBoss>(this, new BulletBossIdle());
        //bossFsm = new BossStateMachine<BulletBoss>(this, new StraightBullet());
        bossFsm.AddStateList(new CircleBullet());
        bossFsm.AddStateList(new StraightBullet());
        bossFsm.AddStateList(new FirecrackerBullet());
    }
    public GameObject InstantiateObj(GameObject obj, Transform trm, Quaternion rot)
    {
        return Instantiate(obj, trm.position, rot);
    }
    public void DestroyObj(GameObject obj)
    {
        Destroy(obj);
    }
        
}
