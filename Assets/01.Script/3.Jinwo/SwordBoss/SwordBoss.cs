using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBoss : BossBase<SwordBoss>
{
    private void Awake()
    {

        //bossFsm = new StateMachine<DashEnemy>(this, new StateMove<DashEnemy>());


    }

    protected override void Update()
    {
        base.Update();

    }
    public void ChangeAttack()
    {

    }

}
