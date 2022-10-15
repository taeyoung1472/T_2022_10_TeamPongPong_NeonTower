using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSpinningAttack<T> : BossState<T> where T : BossBase<T>
{
    public override void Enter()
    {
    }

    public override void Execute()
    {
        //var size = Physics.SphereCastNonAlloc(attackRoot.position, attackRadius, direction, hits, deltaDistance,
        //       whatIsTarget);
    }

    public override void Exit()
    {
    }

}
