using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : BossBase<Sword>
{
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        Gizmos.DrawSphere(transform.position, data.attackRange);
    }
#endif
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
