using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SummonerBoss : BossBase<SummonerBoss>
{
    private readonly string _startAnim = "StartAnimation";
    public string StartAnim => _startAnim;
    [SerializeField]
    private float _speed = 8f;
    [SerializeField]
    private GameObject _laserModel = null;
    public GameObject LaserModel => _laserModel;

    [SerializeField]
    private SummonerAttackDataSO _summonerAttackDataSO = null;
    public SummonerAttackDataSO AttackDataSO => _summonerAttackDataSO;

    private float _slowCooltime = 0f;
    private float _summonCooltime = 0f;

    public float SlowCooltime
    {
        get => _slowCooltime;
        set => _slowCooltime = value;
    }
    public float SummonCooltime
    {
        get => _summonCooltime;
        set => _summonCooltime = value;
    }

    private Collider _col = null;
    public Collider Col => _col;

    private void Start()
    {
        _col = GetComponent<Collider>();
        animator = transform.GetChild(0).Find("Model").GetComponent<Animator>();
        CurHp = Data.maxHp;
        _laserModel.SetActive(false);
        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = true;
        agent.updateRotation = false;
        _col.enabled = false;
        agent.speed = _speed;

        bossFsm = new BossStateMachine<SummonerBoss>(this, new SummonerStartState());
        bossFsm.AddStateList(new SummonerAttack());
        bossFsm.AddStateList(new SummonerSkillLaser());
        bossFsm.AddStateList(new SummonerSkillSlow());
        bossFsm.AddStateList(new SummonerSkillSummon());
        bossFsm.AddStateList(new SummonerIdle());
        bossFsm.AddStateList(new SummonerWalk());
    }

    protected override void Update()
    {
        base.Update();
        _slowCooltime += Time.deltaTime;
        _summonCooltime += Time.deltaTime;  
    }

    public override void ApplyDamage(int dmg)
    {
        DamagePopup.PopupDamage(transform.position + Vector3.up * 1.3f, dmg);
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

    public void GoIdleState()
    {
        bossFsm.ChangeState<SummonerIdle>();
    }

    public GameObject MonoInstantiate(GameObject obj, Vector3 position, Quaternion rotation)
    {
        GameObject gameObject = Instantiate(obj, position, rotation);
        return gameObject;
    }

    public void ModelReset()
    {
        Animator.transform.localRotation = Quaternion.identity;
    }

    public void MonoDestroy(GameObject obj, float time)
    {
        Destroy(obj, time);
    }
}

public class SummonerStartState : BossState<SummonerBoss>
{
    public override void Enter()
    {
        stateMachineOwnerClass.StartCoroutine(StartAnimationCoroutine());
    }
    private IEnumerator StartAnimationCoroutine()
    {
        stateMachineOwnerClass.Animator.Play(stateMachineOwnerClass.StartAnim);
        stateMachineOwnerClass.Animator.Update(0);
        yield return new WaitUntil(() => stateMachineOwnerClass.Animator.GetCurrentAnimatorStateInfo(0).IsName(stateMachineOwnerClass.StartAnim) == false);
        stateMachineOwnerClass.Col.enabled = true;
        stateMachine.ChangeState<SummonerIdle>();
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
    }
}