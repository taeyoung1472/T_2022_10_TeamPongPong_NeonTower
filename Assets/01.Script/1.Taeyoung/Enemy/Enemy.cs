using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : PoolAbleObject, IDamageable, IObserver
{
    protected float health;// 현재 체력
    protected bool dead = false; // 사망 상태
    public float Health { get => health; set => health = value; } // 현재 체력
    public bool Dead { get => dead; set => dead = value; } // 사망 상태

    public UnityEvent OnDeath; // 사망시 발동할 이벤트

    public float lastAttackTime = 0;

    Vector3 baseScale;



    [SerializeField]
    protected EnemyDataSO enemyData;

    public EnemyDataSO EnemyData
    {
        get => enemyData;
        set => enemyData = value;
    }

    protected GameObject target;
    public GameObject Target
    {
        get => target;
        set => target = value;
    }

    protected LayerMask whatIsTarget; // 추적 대상 레이어
    protected Transform attackRoot;

    protected bool isAttack = false;

    public bool IsAttack { get { return isAttack; } set { isAttack = value; } }

    protected const float minTimeBetDamaged = 0.1f;
    protected float lastDamagedTime;

    // 잠깐 무적 시간
    protected bool IsInvulnerable
    {
        get
        {
            if (Time.time >= lastDamagedTime + minTimeBetDamaged) return false;

            return true;
        }
    }

    public virtual void Die()
    {
        dead = true;

        // onDeath 이벤트에 등록된 메서드가 있다면 실행
        Define.Instance.playerController.StealHp();
        AudioManager.PlayAudioRandPitch(enemyData.deathClip);
        GameObject obj = PoolManager.Instance.Pop(PoolType.EXPBall).gameObject;
        GameObject dieEffect = PoolManager.Instance.Pop(PoolType.EnemyDeadEffect).gameObject;
        dieEffect.transform.position = transform.position;
        obj.transform.position = transform.position;
        if (OnDeath != null) OnDeath?.Invoke();
    }

    public void Init(Vector3 initPos, GameObject target)
    {
        GetComponent<NavMeshAgent>().enabled = false;
        transform.position = initPos;
        Target = target.gameObject;
        GetComponent<NavMeshAgent>().enabled = true;
    }

    #region PoolAble
    public override void Init_Pop()
    {
        if (WaveManager.Instance)
        {
            health = enemyData.floorPerPerformance[WaveManager.Instance.CurFloor - 1].maxHp;
        }
        else
        {
            health = 2;
        }
        dead = false;
        baseScale = transform.localScale;
        EnemySubject.Instance.RegisterObserver(this);
    }

    public override void Init_Push()
    {
        transform.localScale = baseScale;
    }

    public virtual void ApplyDamage(float dmg)
    {
        health -= dmg;
        AudioManager.PlayAudioRandPitch(enemyData.hitClip);
        DamagePopup.PopupDamage(transform.position, dmg);
        if (health <= 0)
        {
            Die();
        }
    }

    public void ObserverUpdate()
    {
        Die();
    }
    #endregion
}
