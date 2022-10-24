using MoreMountains.Tools;
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

    [Header("[����]")]
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private AudioClip smallFireClip;
    [SerializeField] private AudioClip bigFireClip;
    [SerializeField] private AudioClip boomClip;

    public AudioClip SmallFireClip => smallFireClip;
    public AudioClip BigFireClip => bigFireClip;
    public AudioClip BoomClip => boomClip;

    # region ���� ���� ����
    [Header("���� ���� ������")]
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

    #region ���� ���� ����
    [Header("���� ���� ������")]
    [SerializeField] private float circlePower = 5f;
    [SerializeField] private int fireCircleCnt = 15;
    public float CirclePower => circlePower;
    public int FireCircleCnt => fireCircleCnt;
    #endregion

    #region ���� ���� ����
    [Header("���� ���� ������")]
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

    #region �ڰ��� ���� ����
    [Header("�ڰ��� ���� ������")]

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

    [Header("������Ʈ���� ���̵�� �Ѿ�� �ð�")]
    [SerializeField] private float stateToIdleTime = 1f;

    public float StateToIdleTime => stateToIdleTime;

    public List<int> randomIndexList = new List<int>();
    public List<int> bowlIndexList = new List<int>();

    [SerializeField] private VisualEffect sparkParticles;
    [SerializeField] private Animator sparkAnimator;
    [SerializeField] private GameObject flyDestrotObject;
    [SerializeField] private GameObject destroySPHERE;
    [SerializeField] private GameObject sparkObj;
    public int RandomIndex()
    {
        int returnValue = 0;

        if (bowlIndexList.Count != 0) //�׸� �ִٴ�
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

        bossFsm = new BossStateMachine<BulletBoss>(this, new StartWaitState()); //���·� �������� ��ٸ��� ����
        //bossFsm = new BossStateMachine<BulletBoss>(this, new BulletBossIdle());
        bossFsm.AddStateList(new BulletBossIdle()); //�⺻����
        bossFsm.AddStateList(new CircleBullet()); // ������ ���� �Ϲ� �Ѿ� ������ �߻� 
        bossFsm.AddStateList(new StraightBullet());// ���� ���� �Ϲ��Ѿ� 4�߾� 3��
        bossFsm.AddStateList(new FirecrackerBullet()); //���� ���� ū �Ѿ��ϳ� ������ ������ �� ������ �� �� ������
        bossFsm.AddStateList(new StraightMotar()); //���� �ڰ��� ���� 1 2 3 4 5 ������ ����ǥ�� �ڰ���
        bossFsm.AddStateList(new PlayerMotar()); //�÷��̾������� ���� �� �ڰ��� ����

        bossFsm.AddStateList(new CircleMotar()); //������ 1 2 3 �ڰ��� ����
        bossFsm.AddStateList(new BulletBossDie()); //�׾����� ����
    }
    public override void ApplyDamage(float dmg)
    {
        DamagePopup.PopupDamage(transform.position + Vector3.up * 1.3f, dmg);
        CurHp -= dmg;
        AudioManager.PlayAudioRandPitch(hitClip);
        BossUIManager.BossDamaged();
        if (CurHp <= 0)
        {
            Debug.Log("��� !!");
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
        sparkObj.SetActive(true);
        sparkAnimator.Play("BombExplosion");
        sparkParticles.Play();
        yield return new WaitForSecondsRealtime(1f);
        flyDestrotObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(1f, 3f),
            Random.Range(1f, 3f), Random.Range(1f, 3f)), ForceMode.Impulse);
        yield return new WaitForSeconds(1f);
       //Destroy(GameManager.Instance.a);
        Debug.Log("������Ϸ�");
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
