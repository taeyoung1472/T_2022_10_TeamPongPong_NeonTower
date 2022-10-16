using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BulletBoss : BossBase<BulletBoss>
{
    public GameObject bullet;
    public GameObject firecrackerBullet;
    public GameObject backBullet;
    private void Start()
    {
        bossFsm = new BossStateMachine<BulletBoss>(this, new BulletBossIdle());
        //bossFsm = new BossStateMachine<BulletBoss>(this, new StraightBullet());
        bossFsm.AddStateList(new CircleBullet());
        bossFsm.AddStateList(new StraightBullet());
        bossFsm.AddStateList(new FirecrackerBullet());
        bossFsm.AddStateList(new StraightMotar());
        bossFsm.AddStateList(new PlayerMotar());
        bossFsm.AddStateList(new CircleMotar());
    }
    public GameObject InstantiateObj(GameObject obj, Vector3 pos, Quaternion rot)
    {
        return Instantiate(obj, pos, rot);
    }
    public void DestroyObj(GameObject obj)
    {
        Destroy(obj);
    }
        
}
