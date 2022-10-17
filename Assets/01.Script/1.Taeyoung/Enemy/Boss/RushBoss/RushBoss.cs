using UnityEngine;
using UnityEngine.AI;

public class RushBoss : BossBase<RushBoss>
{
    [SerializeField]
    private RushBossAttackDataSO _attackDataSO = null;
    public RushBossAttackDataSO AttackDataSO => _attackDataSO;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        CurHp = Data.maxHp;
        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = true;
        agent.updateRotation = true;
        agent.speed = Data.speed;

        bossFsm = new BossStateMachine<RushBoss>(this, new Idle_RushBoss<RushBoss>());
        bossFsm.AddStateList(new MeleeAttack_RushBoss<RushBoss>());
        bossFsm.AddStateList(new WaveAttack_RushBoss<RushBoss>());
        bossFsm.AddStateList(new RushAttack_RushBoss<RushBoss>());
        bossFsm.AddStateList(new JumpAttack_RushBoss<RushBoss>());
        bossFsm.AddStateList(new Move_RushBoss<RushBoss>());

        StadiumManager.Instance.GetStadiumByType(BossType.Boss2).Active();
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
            Debug.Log("»ç¸Á !!");
            StopAllCoroutines();
            OnDeathEvent?.Invoke();
            Destroy(gameObject);
        }
    }
}
