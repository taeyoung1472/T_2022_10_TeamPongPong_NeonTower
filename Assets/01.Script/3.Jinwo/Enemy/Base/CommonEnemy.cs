using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonEnemy : EnemyBase<CommonEnemy>
{


    //protected StateMachine<CommonEnemy> fsmManager;
    //public StateMachine<CommonEnemy> FsmManager => fsmManager;
    protected override void Awake()
    {
        base.Awake();

        fsmManager = new StateMachine<CommonEnemy>(this, new StateMove<CommonEnemy>());

        fsmManager.AddStateList(new StateMeleeAttack<CommonEnemy>());

        //fsmManager.ReturnDic();

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

    public override void DisableAttack()
    {
        base.DisableAttack();
        //Debug.Log("change");
        FsmManager.ChangeState<StateMove<CommonEnemy>>();
    }
}
