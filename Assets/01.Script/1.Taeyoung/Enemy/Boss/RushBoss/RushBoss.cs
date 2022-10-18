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
        bossFsm.AddStateList(new MeleeAttack_RushBoss<RushBoss>());
        bossFsm.AddStateList(new WaveAttack_RushBoss<RushBoss>());
        bossFsm.AddStateList(new RushAttack_RushBoss<RushBoss>());
        bossFsm.AddStateList(new JumpAttack_RushBoss<RushBoss>());
        bossFsm.AddStateList(new Move_RushBoss<RushBoss>());

        //StadiumManager.Instance.GetStadiumByType(BossType.Boss2).Active();
    }

    protected override void Update()
    {
        TargetLook();
        base.Update();
    }

    public override void ApplyDamage(float dmg)
    {
        DamagePopup.PopupDamage(transform.position + Vector3.up, dmg);
        CurHp -= dmg;
        BossUIManager.BossDamaged();
        if (CurHp <= 0)
        {
            Debug.Log("»ç¸Á !!");
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


    public void DoPunch(int index)
    {
        switch (index)
        {
            case 1:
                StartCoroutine(PunchCoroutine());
                break;
        }
    }

    private IEnumerator PunchCoroutine()
    {
        DangerZone.DrawArc(transform.position, transform.forward, 180f, new Vector3(4f, 0f, 4f), 0.2f);
        yield return new WaitForSeconds(0.2f);
    }
}
