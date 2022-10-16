using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SummonerBoss : BossBase<SummonerBoss>
{
    private readonly string _startAnim = "StartAnimation";
    public string StartAnim => _startAnim;
    [SerializeField]
    private GameObject _laserModel = null;
    public GameObject LaserModel => _laserModel;

    private void Start()
    {
        animator = transform.GetChild(0).Find("Model").GetComponent<Animator>();
        CurHp = Data.maxHp;
        _laserModel.SetActive(false);
        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = true;
        agent.updateRotation = false;

        bossFsm = new BossStateMachine<SummonerBoss>(this, new SummonerStartState());
        bossFsm.AddStateList(new SummonerIdle());
        bossFsm.AddStateList(new SummonerWalk());
    }


    public override void ApplyDamage(int dmg)
    {
        DamagePopup.PopupDamage(transform.position + Vector3.up * 1.3f, dmg);
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

    public void TargetLook()
    {
        Vector3 distance = Target.position - transform.position;
        Quaternion rot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(distance.normalized), Time.deltaTime * 20f);
        transform.rotation = rot;
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
        stateMachine.ChangeState<SummonerIdle>();
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
    }
}
