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

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        CurHp = Data.maxHp;
        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = true;
        agent.updateRotation = false;
        agent.speed = Data.speed;

        bossFsm = new BossStateMachine<RushBoss>(this, new Idle_RushBoss<RushBoss>());
        bossFsm.AddStateList(new MeleeAttack_RushBoss<RushBoss>()); // 3�� ������
        bossFsm.AddStateList(new WaveAttack_RushBoss<RushBoss>()); // ���� Ÿ��
        bossFsm.AddStateList(new RushAttack_RushBoss<RushBoss>()); // �̳� �޸���
        bossFsm.AddStateList(new GroundPoundAttack_RushBoss<RushBoss>()); // �ٴ� �����ϸ� ������
        bossFsm.AddStateList(new Move_RushBoss<RushBoss>()); // ���� �����̱�

        //StadiumManager.Instance.GetStadiumByType(BossType.Boss2).Active();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void ApplyDamage(int dmg)
    {
        DamagePopup.PopupDamage(transform.position + Vector3.up, dmg);
        CurHp -= (float)dmg;
        BossUIManager.BossDamaged();
        if (CurHp <= 0)
        {
            Debug.Log("��� !!");
            StopAllCoroutines();
            OnDeathEvent?.Invoke();
            Destroy(gameObject);
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
