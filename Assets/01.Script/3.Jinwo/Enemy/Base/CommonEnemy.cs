using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonEnemy : EnemyBase
{


    protected StateMachine<CommonEnemy> fsmManager;
    public StateMachine<CommonEnemy> FsmManager => fsmManager;


    void Awake()
    {

        fsmManager = new StateMachine<CommonEnemy>(this, new StateMove<CommonEnemy>());

        fsmManager.AddStateList(new StateMeleeAttack());

    }

    void Update()
    {
        fsmManager.Execute();
        Debug.Log(fsmManager.getNowState.ToString());

    }
}
