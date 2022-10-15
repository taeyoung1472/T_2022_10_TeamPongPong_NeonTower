using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEnemy : EnemyBase<DashEnemy>
{
    protected override void Awake()
    {
        base.Awake();

        fsmManager = new StateMachine<DashEnemy>(this, new StateMove<DashEnemy>());

        fsmManager.AddStateList(new StateDashAttack<DashEnemy>());
        fsmManager.AddStateList(new StateMeleeAttack<DashEnemy>());
        //fsmManager.AddStateList(new StateMeleeAttack<DashEnemy>());

        //fsmManager.ReturnDic();

    }

    void Update()
    {
        fsmManager.Execute();
    }
    public override void ChangeAttack()
    {
        FsmManager.ChangeState<StateDashAttack<DashEnemy>>();
    }


    public override void DisableAttack()
    {
        base.DisableAttack();
        //Debug.Log("change");
        FsmManager.ChangeState<StateMove<DashEnemy>>();
    }
}
