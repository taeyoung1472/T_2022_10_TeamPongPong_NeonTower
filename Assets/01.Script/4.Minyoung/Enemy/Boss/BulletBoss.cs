using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BulletBoss : BossBase<BulletBoss>
{
    [SerializeField] private PoolAbleObject bullet;
    [SerializeField] private PoolAbleObject firecrackerBullet;
    [SerializeField] private PoolAbleObject motarBullet;

    public Transform rotateTrm;

    # region 직선 공격 변수
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
    #endregion

    #region 원형 공격 변수
    [Header("원형 공격 변수들")]
    [SerializeField] private float circlePower = 5f;
    [SerializeField] private int fireCircleCnt = 15;
    public float CirclePower => circlePower;
    public int FireCircleCnt => fireCircleCnt;
    #endregion

    #region 폭죽 공격 변수
    [Header("폭죽 공격 변수들")]
    [SerializeField] private float bigBulletDestoryTime = 3f;
    [SerializeField] private int boomCircleCnt = 12;
    [SerializeField] private float boomPower = 7f;
    [SerializeField] private float boomCirclePower = 5f;
    [SerializeField] private float boomDelay = 0.5f;
    public float BigBulletDestoryTime => bigBulletDestoryTime;
    public float BoomCircleCnt => boomCircleCnt;
    public float BoomCirclePower => boomCirclePower;
    public float BoomPower => boomPower;
    public float BoomDelay => boomDelay;
    #endregion

    #region 박격포 공격 변수
    [Header("박격포 공격 변수들")]

    [SerializeField] private float motarDownPower = 8f;
    [SerializeField] private float waringBoomTime = 0.5f;

    [SerializeField] private int firstMotarCnt = 5;
    [SerializeField] private int secontMotarCnt = 7;
    [SerializeField] private int thirdMotarCnt = 10;

    public float MotarDownPower => motarDownPower;
    public float WaringBoomTime => waringBoomTime;
    public int FirstMotarCnt => firstMotarCnt;
    public int SecontMotarCnt => secontMotarCnt;
    public int ThirdMotarCnt => thirdMotarCnt;
    #endregion

    [Header("스테이트에서 아이들로 넘어가는 시간")]
    [SerializeField] private float stateToIdleTime = 1f;

    public float StateToIdleTime => stateToIdleTime;

    public List<int> randomIndexList = new List<int>();
    public List<int> bowlIndexList = new List<int>();

    [SerializeField] private VisualEffect sparkParticles;
    [SerializeField] private Animator sparkAnimator;
    [SerializeField] private GameObject flyDestrotObject;   
    public int RandomIndex()
    {
        int returnValue = 0;

        if(bowlIndexList.Count != 0) //그릇 있다는
        {
            int rand = Random.Range(0, bowlIndexList.Count); 
            returnValue = bowlIndexList[rand]; 
            bowlIndexList.RemoveAt(rand);
        }
        else
        {
            bowlIndexList.AddRange(randomIndexList);

            int rand = Random.Range(0, bowlIndexList.Count);
            returnValue = bowlIndexList[rand];
            bowlIndexList.RemoveAt(rand);
        }

        return returnValue;
    }
    private void Start()
    {
        CurHp = Data.maxHp;

        bossFsm = new BossStateMachine<BulletBoss>(this, new StartWaitState());
        //bossFsm = new BossStateMachine<BulletBoss>(this, new BulletBossIdle());
        bossFsm.AddStateList(new BulletBossIdle());
        bossFsm.AddStateList(new CircleBullet());
        bossFsm.AddStateList(new StraightBullet());
        bossFsm.AddStateList(new FirecrackerBullet());
        bossFsm.AddStateList(new StraightMotar());
        bossFsm.AddStateList(new PlayerMotar());

        bossFsm.AddStateList(new CircleMotar());
        bossFsm.AddStateList(new BulletBossDie());
    }
    public override void ApplyDamage(float dmg)
    {
        DamagePopup.PopupDamage(transform.position + Vector3.up * 1.3f, dmg);
        CurHp -= dmg;
        BossUIManager.BossDamaged();
        if (CurHp <= 0)
        {
            Debug.Log("사망 !!");
            StopAllCoroutines();
            OnDeathEvent?.Invoke();
            bossFsm.ChangeState<BulletBossDie>();
        }
    }
    public override void LookTarget()
    {
        rotateTrm.rotation = Quaternion.LookRotation(Target.position - rotateTrm.position);
    }

    public void BulletBossDieEffect()
    {
        StartCoroutine(BulletBossDieEffectCoroutine());
    }
    private IEnumerator BulletBossDieEffectCoroutine()
    {
        sparkAnimator.Play("BombExplosion");
        sparkParticles.Play();
        flyDestrotObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(1f, 3f), 
            Random.Range(1f, 3f), Random.Range(1f, 3f)), ForceMode.Impulse);
        Debug.Log("개고생완료");
        yield return null;
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
