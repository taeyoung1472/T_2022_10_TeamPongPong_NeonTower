using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BulletBoss : BossBase<BulletBoss>
{
    public GameObject bullet;
    public GameObject firecrackerBullet;
    public GameObject backBullet;

    [Header("직선 공격 변수들")]
    [SerializeField] private float straightPower = 7f;
    [SerializeField] private float interval = 0.3f;
    [SerializeField] private float cycleInterval = 1f;
    [SerializeField] private int cycleCnt = 3;
    [SerializeField] private int fireCnt = 4;

    public int CycleCnt => cycleCnt;
    public int FireCnt => fireCnt;
    public float StraightPower => straightPower;
    public float Interval => interval;
    public float CycleInterval => cycleInterval;

    [Header("원형 공격 변수들")]
    [SerializeField] private float circlePower = 5f;
    [SerializeField] private int fireCircleCnt = 15;
    public float CirclePower => circlePower;
    public int FireCircleCnt => fireCircleCnt;
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
