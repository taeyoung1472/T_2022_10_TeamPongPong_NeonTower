using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RushBoss : BossBase<RushBoss>
{
    [SerializeField]
    private RushBossAttackDataSO _attackDataSO = null;
    public RushBossAttackDataSO AttackDataSO => _attackDataSO;
    [SerializeField]
    private GameObject _model = null;
    public GameObject Model => _model;

    private SkinnedMeshAfterImage _after = null;
    public SkinnedMeshAfterImage After => _after;

    private Collider _col = null;
    public Collider Col => _col;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        CurHp = Data.maxHp;
        _after = GetComponent<SkinnedMeshAfterImage>();
        _col = GetComponent<Collider>();
        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = true;
        agent.updateRotation = false;
        agent.speed = Data.speed;

        bossFsm = new BossStateMachine<RushBoss>(this, new StartAnimation_RushBoss<RushBoss>());
        bossFsm.AddStateList(new MeleeAttack_RushBoss<RushBoss>()); // 3�� ������
        bossFsm.AddStateList(new WaveAttack_RushBoss<RushBoss>()); // ���� Ÿ��
        bossFsm.AddStateList(new RushAttack_RushBoss<RushBoss>()); // �̳� �޸���
        bossFsm.AddStateList(new GroundPoundAttack_RushBoss<RushBoss>()); // �ٴ� �����ϸ� ������
        bossFsm.AddStateList(new Move_RushBoss<RushBoss>()); // ���� �����̱�
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
            Debug.Log("��� !!");
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
