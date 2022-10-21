using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class RushBoss : BossBase<RushBoss>
{
    [SerializeField]
    private Material _mat = null;
    public Material Mat => _mat;
    [SerializeField]
    private RushBossAttackDataSO _attackDataSO = null;
    public RushBossAttackDataSO AttackDataSO => _attackDataSO;
    [SerializeField]
    private GameObject _model = null;
    public GameObject Model => _model;
    [SerializeField]
    private GameObject _rushForceField = null;
    public GameObject RushForceField => _rushForceField;

    private SkinnedMeshAfterImage _after = null;
    public SkinnedMeshAfterImage After => _after;

    private Collider _col = null;
    public Collider Col => _col;


    public float radius = 0f;
    public float angle = 0f;

    private GameObject _attackPositionObj = null;
    public GameObject AttackPositionObj => _attackPositionObj;

    [SerializeField]
    private ParticleSystem _punchParticle = null;
    [SerializeField]
    private GameObject _arcAttackParticle = null;
    [SerializeField]
    private ParticleSystem[] _thunderParticle = null;
    [SerializeField]
    private GameObject _explosionEffect = null;

    private Coroutine co = null;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        Mat.SetFloat("_DissolveBeginOffset", 1f);
        _attackPositionObj = new GameObject("AttackPositionObj");
        CurHp = Data.maxHp;
        _after = GetComponent<SkinnedMeshAfterImage>();
        _col = GetComponent<Collider>();
        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = true;
        agent.updateRotation = false;
        agent.speed = Data.speed;

        bossFsm = new BossStateMachine<RushBoss>(this, new StartAnimation_RushBoss<RushBoss>());
        bossFsm.AddStateList(new MeleeAttack_RushBoss<RushBoss>()); // 3대 때리기
        bossFsm.AddStateList(new WaveAttack_RushBoss<RushBoss>()); // 원형 타격
        bossFsm.AddStateList(new RushAttack_RushBoss<RushBoss>()); // 겁내 달리기
        bossFsm.AddStateList(new GroundPoundAttack_RushBoss<RushBoss>()); // 바닥 쩜프하며 때리기
        bossFsm.AddStateList(new Move_RushBoss<RushBoss>()); // 그저 움직이기
        bossFsm.AddStateList(new Die_RushBoss<RushBoss>());
        bossFsm.AddStateList(new Idle_RushBoss<RushBoss>());

        //StadiumManager.Instance.GetStadiumByType(BossType.Boss2).Active();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void ApplyDamage(float dmg)
    {
        DamagePopup.PopupDamage(transform.position + Vector3.up, dmg);
        CurHp -= dmg;
        BossUIManager.BossDamaged();
        if (CurHp <= 0)
        {
            Debug.Log("사망 !!");
            StopAllCoroutines();
            OnDeathEvent?.Invoke();
            bossFsm.ChangeState<Die_RushBoss<RushBoss>>();
        }
    }

    public void TargetLook()
    {
        Vector3 distance = Target.position - transform.position;
        Quaternion rot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(distance.normalized), Time.deltaTime * 20f);
        transform.rotation = rot;
    }
    public void ModelReset()
    {
        Animator.transform.localRotation = Quaternion.identity;
    }

    public void GoIdleState()
    {
        bossFsm.ChangeState<Idle_RushBoss<RushBoss>>();
    }

    public float GetDistance()
    {
        Vector3 targetPosition = Target.position;
        targetPosition.y = transform.position.y;

        return Vector3.Distance(targetPosition, transform.position);
    }

    private void OnDrawGizmos()
    {
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, angle/2, radius);
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -angle/2, radius);
    }

    public void PunchParticlePlay()
    {
        if (co != null)
            StopCoroutine(co);
        co = StartCoroutine(PunchCo());
    }

    private IEnumerator PunchCo()
    {
        _punchParticle.gameObject.SetActive(true);
        _punchParticle.Play();
        yield return new WaitForSeconds(2f);
        _punchParticle.Stop();
        _punchParticle.gameObject.SetActive(false);
    }

    public void ThunderParticlePlay()
    {
        //for(int i = 0; i <_thunderParticle.Length; i++)
        //{
        //    _thunderParticle[i].Play();
       // }
    }

    public void StopParticle()
    {
        if (co != null)
            StopCoroutine(co);
        _punchParticle.Stop();
        _punchParticle.gameObject.SetActive(false);

        for (int i = 0; i < _thunderParticle.Length; i++)
        {
            _thunderParticle[i].Stop();
        }
    }

    public void ExplosionEffect(Vector3 pos)
    {
        GameObject obj = Instantiate(_explosionEffect, pos, Quaternion.identity);
        Destroy(obj, 1f);
    }

    public void ArcAttack()
    {
        GameObject obj = Instantiate(_arcAttackParticle, transform);
        Destroy(obj, 0.4f);
    }
}

public class StartAnimation_RushBoss<T> : BossState<RushBoss> where T : BossBase<T>
{
    public override void Enter()
    {
        stateMachineOwnerClass.After.isMotionTrail = false;
        stateMachineOwnerClass.Col.enabled = false;
        stateMachineOwnerClass.StartCoroutine(WaitStartAnimation());
    }

    private IEnumerator WaitStartAnimation()
    {
        stateMachineOwnerClass.Animator.Play("Start");
        stateMachineOwnerClass.Animator.Update(0);
        yield return new WaitUntil(() =>
        stateMachineOwnerClass.Animator.GetCurrentAnimatorStateInfo(0).IsName("Start") == false);
        stateMachine.ChangeState<Idle_RushBoss<RushBoss>>();
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
        stateMachineOwnerClass.Col.enabled = true;
    }

}
