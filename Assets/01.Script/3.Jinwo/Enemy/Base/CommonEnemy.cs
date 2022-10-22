using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonEnemy : EnemyBase<CommonEnemy>
{

    [SerializeField]
    private ParticleSystem particle;
    //protected StateMachine<CommonEnemy> fsmManager;
    //public StateMachine<CommonEnemy> FsmManager => fsmManager;
    protected override void Awake()
    {
        base.Awake();

        fsmManager = new StateMachine<CommonEnemy>(this, new StateMove<CommonEnemy>());

        fsmManager.AddStateList(new StateMeleeAttack<CommonEnemy>());

        //fsmManager.ReturnDic();

    }
    private void Start()
    {
        particle.gameObject.SetActive(false);
    }
    void Update()
    {
        fsmManager.Execute();
        //Debug.Log(fsmManager.getNowState.ToString());

    }

    public override void ChangeAttack()
    {
        //Debug.Log("자식 실행");
        FsmManager.ChangeState<StateMeleeAttack<CommonEnemy>>();
    }
    public override void EnableAttack()
    {
        base.EnableAttack();
        particle.gameObject.SetActive(true);
        particle.Play();
    }
    public override void DisableAttack()
    {
        base.DisableAttack();
        //Debug.Log("change");
        particle.gameObject.SetActive(false);
        FsmManager.ChangeState<StateMove<CommonEnemy>>();
    }
}
